/***************************************************************
*file: EnemyElementalEffects.cs
*author: G. Valdiviez
*class: CS 4700 – Game Development
*assignment: Final Program
*date last modified: 11/21/2026
*
*purpose: This is how enemies are affected by elemental damage types.
*         Made in a separate script to make this more universal.
*
****************************************************************/
using Unity.VisualScripting;
using UnityEngine;

public class EnemyElementalEffects : MonoBehaviour
{
    [Header("Freeze Status Variables")]
    public bool IsFrozen { get; private set; }
    private float freezeTimer;

    [Header("Burn Status Variables")]
    public bool IsBurning { get; private set; }
    private float burnTimer;
    private float burnTickTimer;
    private float burnDamagePerTick;
    private EnemyHealth enemyHealth;

    // function: Start
    // Purpose: Called on the frame when this script is enabled.
    //          Gives the variable enemyHealth it's EnemyHealth component.
    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }

    // function: Update
    // Purpose: Checks to see if the enemy is afflicted by a status effect.
    //          Calls other functions accordingly.
    void Update()
    {
        // Checks if Frozen
        if (IsFrozen)
        {
            freezeTimer -= Time.deltaTime;

            if (freezeTimer <= 0f)
            {
                IsFrozen = false;
                freezeTimer = 0f;
            }
        }


        if (IsBurning)
        {
            burnTimer -= Time.deltaTime;
            burnTickTimer -= Time.deltaTime;

            if(burnTickTimer <= 0f)
            {
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(burnDamagePerTick);
                }

                burnTickTimer += 1f;
            }

            if (burnTimer <= 0f)
            {
                IsBurning = false;
                burnTimer = 0f;
            }
        }
    }

    // Function: ApplyFreeze
    // Purpose: Upon being hit with an ice DamageType projectile, applies freeze to enemy.
    public void ApplyFreeze(float duration)
    {
        if (duration <= 0f || IsFrozen)
        {
            return;
        }

        IsFrozen = true;
        freezeTimer = Mathf.Max(freezeTimer, duration);
    }

    public void ApplyBurn(float duration, float damagePerTick)
    {
        if (duration <= 0f || damagePerTick <= 0f || IsBurning)
        {
            return;
        }

        IsBurning = true;
        burnTimer = duration;
        burnTickTimer = 1f;
        burnDamagePerTick = damagePerTick;
    }
}
