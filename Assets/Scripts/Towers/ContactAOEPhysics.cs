/***************************************************************
*file: ContactAOEPhysics.cs
*author: G. Valdiviez
*class: CS 4700 – Game Development
*assignment: Final Program
*date last modified: 11/24/2026
*
*purpose: Contains the physics behind the Contact AOE projectiles.
*         Fires just like a single shot projectile, but on contact it emits an AOE attack, damaging surrounding enemies.
*         Fires within the base's range and AOE radius is determined by the core's areaOfEffect data.
*
****************************************************************/
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;

public class ContactAOEPhysics : MonoBehaviour
{
    [Header("Projectile Attributes")]
    public float speed = 70f;
    public float timeToDespawn;

    [Header("Explosion Visual")]
    public GameObject explosionVisualPrefab;
    public int circleSegments = 64;
    public float effectDuration = 0.3f;

    private Vector2 direction;
    private float damage;
    private float aoeRadius;
    private DamageType damageType = DamageType.Normal;

    // function: Seek
    // Purpose: When called from TowerManager.cs, gives the projectile three pieces of information:
    //          The direction the bullet is being fired.
    //          The base damage of the projectile itself.
    //          The radius of the AOE after the physical projectile hits the enemy.
    public void Seek(Vector2 firingDirection, float dmg, float aoe, DamageType type)
    { 
        direction = firingDirection.normalized;
        damage = dmg;
        aoeRadius = aoe;
        damageType = type;
    }

    // function: Start
    // Purpose: Called on the frame when this script is enabled.
    //          Destroy any bullets that last at least 5 seconds flying.
    void Start()
    {
        Destroy(gameObject, timeToDespawn);
    }

    // function: Update
    // Purpose: Called every frame while the script is active
    //          Checks to see if there is a target, if not the return
    //          If there is then we see how close the projectile is to the enemy.
    //          If the magnitude of the target's position is less than the amount of distance traveled in a frame, call HitTarget and return.
    //          Else translate the bullet in the direction of the enemy by distanceThisFrame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    // function: OnTriggerEnter2D
    // purpose: Called when a projectile makes contact with an enemy.
    //          Located the EnemyHealth component on the hit enemy, and given they have EnemyHealth, it calls Explode() and destroys the physical projectile.
    void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.GetComponentInParent<EnemyHealth>();

        if (enemyHealth == null)
        {
            return;
        }

        Explode();
        Destroy(gameObject);
    }

    // function: Explode()
    // purpose: After colliding with an enemy, the projectile release an AOE attack.
    //          Gathers enemies all around the hit enemy and damages them if they haven't already been damaged by the AOE.
    //          Damage is based on the core's damage stat.
    void Explode()
    {
        Collider2D[] nearbyColliders = Physics2D.OverlapCircleAll(transform.position, aoeRadius);
        HashSet<EnemyHealth> damagedEnemies = new HashSet<EnemyHealth>();
        EnemyHealth lightningOrigin = null;

        foreach (Collider2D nearbyCollider in nearbyColliders)
        {
            EnemyHealth enemy = nearbyCollider.GetComponentInParent<EnemyHealth>();

            if (enemy == null || damagedEnemies.Contains(enemy))
            {
                continue;
            }

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

                if (damageType == DamageType.Lightning && lightningOrigin == null)
                {
                    lightningOrigin = enemy;
                }
            }
        }

        if (damageType == DamageType.Lightning && lightningOrigin != null)
        {
            ChainLightning.Trigger(lightningOrigin, damage, damagedEnemies);
        }

        SpawnExplosionVisual();
    }

    // function: SpawnExplosionVisual()
    // purpose: After colliding with an enemy, the projectile release an AOE attack.
    //          This function is how we visualize the ensuing explosion.
    //          This sets all the parameters for a LineRenderer and sets an effect duration. Using them as arguments for Initialize in ExplosionFade.cs
    void SpawnExplosionVisual()
    {
        if (explosionVisualPrefab == null)
        {
            return;
        }

        GameObject visualObject = Instantiate(explosionVisualPrefab, transform.position, Quaternion.identity);
        SpriteRenderer explosionSprite = visualObject.GetComponent<SpriteRenderer>();

        if (explosionSprite == null || explosionSprite.sprite == null)
        {
            Debug.LogWarning("Explosion Visual Prefab is missing a SpriteRenderer");
            Destroy(visualObject);
            return;
        }

        float spriteDiameter = explosionSprite.sprite.bounds.size.x;

        float desiredDiameter = aoeRadius * 2f;
        float scale = desiredDiameter / spriteDiameter;

        visualObject.transform.localScale = new Vector3(scale, scale, 1f);

        ExplosionFade fade = visualObject.AddComponent<ExplosionFade>();
        fade.Initialize(explosionSprite, effectDuration);
    }
}
