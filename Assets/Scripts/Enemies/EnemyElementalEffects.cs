/***************************************************************
*file: EnemyElementalEffects.cs
*author: G. Valdiviez
*class: CS 4700 – Game Development
*assignment: Final Program
*date last modified: 11/21/2026
*
*purpose: This is how enemies are affected by elemental damage types.
*         Made in a seperate script to make this more universal.
*
****************************************************************/
using UnityEngine;

public class EnemyElementalEffects : MonoBehaviour
{
    public bool IsFrozen { get; private set; }
    private float freezeTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // function: Update
    // Purpose: Checks to see if the enemy is afflicted by a status effect.
    //          Calls other functions accordingly.
    void Update()
    {
        if (!IsFrozen)
        {
            return;
        }

        freezeTimer -= Time.deltaTime;

        if (freezeTimer <= 0f)
        {
            IsFrozen = false;
            freezeTimer = 0f;
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
}
