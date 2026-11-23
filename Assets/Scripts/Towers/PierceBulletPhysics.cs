/***************************************************************
*file: PierceBulletPhysics.cs
*author: G. Valdiviez
*class: CS 4700 – Game Development
*assignment: Final Program
*date last modified: 11/22/2026
*
*purpose: Contains the physics behind the Piercing projectiles.
*         Fires just like a single shot projectile, but it can go through as many enemies as its areaOfEffect data says.
*         So if areaOfEffect = 3, it can hit 3 separate enemies before being Destroyed.
*
****************************************************************/
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PierceBulletPhysics : MonoBehaviour
{
    [Header("Projectile Attributes")]
    public float speed;

    [Header("Pierce Stats")]
    private Vector2 direction;
    private float damage;
    private int pierceLimit;
    private int directHits = 0;
    private HashSet<EnemyHealth> damagedEnemies = new HashSet<EnemyHealth>();

    [Header("Elemental Damage Properties")]
    private DamageType damageType = DamageType.Normal;
    private bool lightningTriggered = false;

    public void Seek(Vector2 firingDirection, float dmg, float pierceAmount, DamageType type)
    {
        direction = firingDirection.normalized;
        damage = dmg;
        damageType = type;

        pierceLimit = Mathf.Max(1, Mathf.RoundToInt(pierceAmount));

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }

    // function: Start
    // Purpose: Called on the frame when this script is enabled.
    //          Destroy any bullets that last at least 5 seconds flying.
    void Start()
    {
        Destroy(gameObject, 5f);
    }


    // function: Update
    // Purpose: Called every frame while the script is active
    //          Translate the bullet in the direction of the Vector2 class variable.
    //          Based on speed, Time.deltaTime, and Space.World
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    // function: OnTriggerEnter2D
    // purpose: Called when a projectile makes contact with an enemy.
    //          This checks to see if pierceLimit has been reached.
    //          If not, the projectile flies and the hit enemy gets added to hitEnemy.
    //          If so, the projectile is destroyed.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemy = collision.GetComponentInParent<EnemyHealth>();

        if (enemy == null)
        {
            return;
        }

        if (damagedEnemies.Contains(enemy))
        {
            return;
        }

        damagedEnemies.Add(enemy);
        directHits++;

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

        if (directHits >= pierceLimit)
        {
            Destroy(gameObject);
        }
    }
}
