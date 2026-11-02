/***************************************************************
*file: TowerManager.cs
*author: G. Valdiviez
*class: CS 4700 – Game Development
*assignment: Final Program
*date last modified: 11/1/2026
*
*purpose: Allows the Towers to function as intended by the development team
*         Rotates a 'partToRotate' to track the nearest enemy at all times (if enemies are nearby)
*         Calls BulletPhysics.cs functions to shoot at the nearest enemy.
*
****************************************************************/

using System;
using Unity.Collections.Tests.CoreCLR.TestJobs;
using Unity.VisualScripting;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    // ScriptableObject Source
    [Header("Tower Data")]
    public TowerData towerData;

    [Header("Unity Setup Fields")]
    public Transform partToRotate; // Part for the tracking
    private Transform target; // Notes what the tower will prioritize shooting

    [Header("Bullet Components")]
    public Transform firePoint;
    private float fireCountdown = 0f;

    // function: Start
    // Purpose: Whenever you boot up the game, it will initialize the variables in towerData.
    void Start()
    {
        towerData.baseDamage = 1f;
        towerData.attackRange = 7f;
        towerData.fireRate = 1f;
        towerData.turnSpeed = 10f;
        towerData.enemyTag = "Enemy";
        InvokeRepeating("UpdateTarget", 0f, 0.1f);
    }

    // function: UpdateTarget
    // Purpose: every tenth of a second, this function will check to see if there is an enemy to lock on to
    //          If there isn't nearest enemy is null, if there is it determines which enemy is closest.
    void UpdateTarget()
    {
        // Finds all enemies within turret's range
        GameObject[] closeEnemies = GameObject.FindGameObjectsWithTag(towerData.enemyTag);
        // Two Temp variables, one holds the distance to the closest enemy, and the other stores the GameObject of the closest enemy
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in closeEnemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            { 
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= towerData.attackRange)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    // function: Update
    // Purpose: Every frame this function does a multitude of things:
    //               - Check to see if there is a targeted enemy, returns if there isn't
    //               - Finds the angle where the target can be located
    //               - If fireCountdown is 0 or less, calls Shoot() and restarts the countdown
    void Update()
    {
        // If there's no target, nothing will update
        if (target == null)
        {
            return;
        }

        // How we know what enemy to track
        Vector2 dir = target.position - partToRotate.position; // Gets direction turret vector from turret to enemy
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; // Calculates the angle. Goes from radians to degrees
        float offset = -90f; // partToRotate starts -90 degrees off, so this is the offset
        Quaternion lookRotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));
        partToRotate.rotation = Quaternion.RotateTowards(partToRotate.rotation, lookRotation, towerData.turnSpeed * Time.deltaTime * 50f); // Rotates towards the target on the Z-axis


        // If you can, Shoot() the enemy
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / towerData.fireRate;
        }
        fireCountdown -= Time.deltaTime; // Every second fireCountdown will decrease
    }

    // function: Shoot
    // Purpose: Conjures a projectilePrefab. Doing so will call the projectile's separate file to see how to track it.
    void Shoot()
    {
        // Creates a temp GameObject for fired bullets
        GameObject firedBullet = (GameObject)Instantiate(towerData.projectilePrefab, firePoint.position, firePoint.rotation);
        BulletPhysics bullet = firedBullet.GetComponent<BulletPhysics>();

        // If a bullet actually IS fired, we call Seek() with target as the argument
        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }
}
