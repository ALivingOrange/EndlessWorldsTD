/***************************************************************
*file: BulletPhysics.cs
*author: G. Valdiviez
*class: CS 4700 – Game Development
*assignment: Final Program
*date last modified: 11/24/2026
*
*purpose: Contains the physics behind a majority of projectile prefabs.
*         Gets the bullet to interact with enemies if they collide.
*         Causes the bullet to despawn if the bullet hits an enemy or it flies for 5 seconds.
*
****************************************************************/

using UnityEngine;

public class BulletPhysics : MonoBehaviour
{
    [Header("Attributes")]
    public float speed = 70f;
    public float timeToDespawn = 5f;
    // Passes on the Target found under TowerManager.cs
    private Vector2 direction;
    private float damage;
    private DamageType damageType = DamageType.Normal;

    // function: Seek
    // Purpose: When called from TowerManager.cs, gives the projectile two pieces of information:
    //          The direction the bullet is being fired.
    //          The data of the firing tower, primarily for baseDamage.
    public void Seek (Vector2 firingDirection, float dmg, DamageType type)
    {
        direction = firingDirection.normalized;
        damage = dmg;
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
    //          Translate the bullet in the direction of the Vector2 class variable.
    //          Based on speed, Time.deltaTime, and Space.World
    void Update()
    {
        // Moves bullet towards target
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    // function: OnTriggerEnter2D
    // Purpose: Detects if the projectile actually collides with an enemy.
    //          If it does then the enemy loses health and the projectile is destroyed.
    //          If not it'll fly until Start() gets to it.
    void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemy = collision.GetComponentInParent<EnemyHealth>();

        if (enemy == null)
        {
            return;
        }

        enemy.TakeDamage(damage);

        EnemyElementalEffects effects = enemy.GetComponentInParent<EnemyElementalEffects>();

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

            if (damageType == DamageType.Lightning)
            {
                ChainLightning.Trigger(enemy, damage);
            }
        }

        Destroy(gameObject);
    }
}
