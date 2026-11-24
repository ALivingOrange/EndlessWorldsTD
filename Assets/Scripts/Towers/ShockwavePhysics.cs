/***************************************************************
*file: ShockwavePhysics.cs
*author: G. Valdiviez
*class: CS 4700 – Game Development
*assignment: Final Program
*date last modified: 11/22/2026
*
*purpose: Contains the physics behind immediate AOE projectiles.
*         Allows the unique projectile to scale up from the center of the tower head and deal damage to any enemies around it.
*         Attacks all over the tower's attack range, and dissipates once outside the range.
*
****************************************************************/
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class ShockwavePhysics : MonoBehaviour
{
    [Header("Attributes")]
    private float maxRadius;
    private float damage;
    private float expansionSpeed = 10f;

    [Header("Radius Settings")]
    private float currentRadius = 0f;
    private float previousRadius = 0f;

    [Header("Initialization Check")]
    private bool initialized = false;

    [Header("Damaged Enemies")]
    private HashSet<EnemyHealth> damagedEnemies = new HashSet<EnemyHealth>();

    [Header("Shockwave Visual")]
    public LineRenderer shockwaveLine;
    public int circleSegments = 64;

    [Header("Element Damage Properties")]
    private DamageType damageType = DamageType.Normal;
    private bool lightningTriggered = false;

    // function: Initialize
    // Purpose: Gives values to maxRadius and damage, and makes initialized true.
    //          Occurs when a ShockwaveHead is placed on a TowerBase
    public void Initialize(float range, float damageAmount, DamageType type)
    {
        maxRadius = range;
        damage = damageAmount;
        initialized = true;
        damageType = type;
    }

    // function: Update
    // Purpose: Called every frame while the script is active.
    //          Every frame this function does a multitude of things:
    //               - Calculates the previous and current radiuses, making currentRadius = maxRadius if currentRadius is larger than maxRadius
    //               - Changes the opacity of the shockwave visual
    //               - Stores information of all nearby enemies and damages them accordingly. Adding enemies already damaged to damagedEnemies HashSet. Won't redamage those enemies.
    //               - If the shockwake is >= maxRadius, destroy the shockwave
    void Update()
    {
        if (!initialized)
        {
            return;
        }

        // Sets previousRadius and currentRadius for frame
        previousRadius = currentRadius;
        currentRadius += expansionSpeed * Time.deltaTime;
        currentRadius = Mathf.Min(currentRadius, maxRadius);
        
        // Visualization of Shockwave.
        DrawShockwave();

        float fadeProgress = currentRadius / maxRadius;
        float alpha = 1f - fadeProgress;

        Color startColor = shockwaveLine.startColor;
        Color endColor = shockwaveLine.endColor;

        startColor.a = alpha;
        endColor.a = alpha;

        shockwaveLine.startColor = startColor;
        shockwaveLine.endColor = endColor;

        // Damages Nearby enemies exactly once per shockwave
        Collider2D[] nearbyColliders = Physics2D.OverlapCircleAll(transform.position, currentRadius);

        foreach (Collider2D nearbyCollider in nearbyColliders)
        {
            EnemyHealth enemy = nearbyCollider.GetComponentInParent<EnemyHealth>();
            
            if (enemy == null || damagedEnemies.Contains(enemy))
            {
                continue;
            }

            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            if (distance >= previousRadius && distance <= currentRadius)
            {
                damagedEnemies.Add(enemy);

                enemy.TakeDamage(damage);

                EnemyElementalEffects effects = enemy.GetComponent<EnemyElementalEffects>();
                if (effects != null)
                {
                    if (damageType == DamageType.Ice)
                    {
                        effects.ApplyFreeze(3f);
                    }
                    else if (damageType == DamageType.Fire)
                    {
                        effects.ApplyBurn(3f, 1f);
                    }
                }

                if (damageType == DamageType.Lightning && !lightningTriggered)
                {
                    lightningTriggered = true;
                    ChainLightning.Trigger(enemy, damage, damagedEnemies);
                }
            }
        }

        if (currentRadius >= maxRadius)
        {
            Destroy(gameObject);
        }
    }

    // function: DrawShockwave
    // Purpose: Creates a visual for the shockwave.
    void DrawShockwave()
    {
        if (shockwaveLine == null)
        {
            return;
        }

        shockwaveLine.positionCount = circleSegments;
        shockwaveLine.loop = true;

        for (int i = 0; i < circleSegments; i++)
        {
            float angle = (float)i / circleSegments * Mathf.PI * 2f;

            float x = Mathf.Cos(angle) * currentRadius;
            float y = Mathf.Sin(angle) * currentRadius;

            shockwaveLine.SetPosition(i, new Vector3(x, y, 0f));
        }
    }

}
