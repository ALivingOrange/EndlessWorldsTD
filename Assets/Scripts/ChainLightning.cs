/***************************************************************
*file: ChainLightning.cs
*author: G. Valdiviez
*class: CS 4700 – Game Development
*assignment: Final Program
*
*purpose: Handles Lightning elemental damage by chaining damage between nearby enemies.
*
****************************************************************/
using UnityEngine;
using System.Collections.Generic;

public static class ChainLightning
{
    // Maximum number of chains (additional enemies that could be hit)
    private const int MAX_CHAINS = 2;

    // Maximum distance between chained enemies
    private const float CHAIN_RANGE = 3f;

    // Modifier of base damage for chained enemies
    private const float CHAIN_DAMAGE_MULTIPLIER = 0.5f;

    // function: FindNearestEnemy
    // Purpose: Acts as a special tracking system for lightning projectiles on contact.
    //          Takes in a HashSet of enemies to avoid to prevent multiple chains on a single enemy, and the currently targeted enemy
    //          We find all nearby enemies (Collider2D's) and determine which is the closest to chain to.
    private static EnemyHealth FindNearestEnemy(EnemyHealth currentEnemy, HashSet<EnemyHealth> hitEnemies)
    {
        EnemyHealth nearestEnemy = null;
        float nearestDistance = CHAIN_RANGE;

        Collider2D[] nearbyColliders = Physics2D.OverlapCircleAll(currentEnemy.transform.position, CHAIN_RANGE);

        foreach (Collider2D collider in nearbyColliders)
        { 
            EnemyHealth enemy = collider.GetComponentInParent<EnemyHealth>();

            if (enemy == null || hitEnemies.Contains(enemy))
            {
                continue;
            }

            float distance = Vector2.Distance(currentEnemy.transform.position, enemy.transform.position);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }

    // function: Trigger
    // Purpose: How we start the lightning chain.
    //          Avoiding damaging the initial enemy again, will chain damage to up to 2 nearby enemies for baseDamage * CHAIN_DAMAGE_MULTIPLIER
    public static void Trigger(EnemyHealth initialEnemy, float originalDamage, HashSet<EnemyHealth> excludedEnemies = null)
    {
        if (initialEnemy == null)
        {
            return;
        }

        HashSet<EnemyHealth> hitEnemies = excludedEnemies != null ? new HashSet<EnemyHealth>(excludedEnemies) : new HashSet<EnemyHealth>();

        hitEnemies.Add(initialEnemy);

        EnemyHealth currentEnemy = initialEnemy;

        for (int i = 0; i < MAX_CHAINS; i++)
        {
            EnemyHealth nextEnemy = FindNearestEnemy(currentEnemy, hitEnemies);

            if (nextEnemy == null)
            {
                return;
            }

            nextEnemy.TakeDamage(originalDamage * CHAIN_DAMAGE_MULTIPLIER);

            hitEnemies.Add(nextEnemy);

            if (excludedEnemies != null)
            {
                excludedEnemies.Add(nextEnemy);
            }

            currentEnemy = nextEnemy;
        }
    }
}
