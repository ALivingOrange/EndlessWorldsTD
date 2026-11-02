/***************************************************************
*file: BulletPhysics.cs
*author: G. Valdiviez
*class: CS 4700 – Game Development
*assignment: Final Program
*date last modified: 11/1/2026
*
*purpose: Contains the physics behind the 'Bullet' Prefab.
*         Gets the bullet to interact with enemies if they collide.
*         Causes the bullet to despawn if the bullet hits an enemy or it flies for 5 seconds.
*
****************************************************************/

using UnityEngine;

public class BulletPhysics : MonoBehaviour
{
    public float speed = 70f;
    // Passes on the Target found under TowerManager.cs
    private Transform target;
    public void Seek (Transform currTarget)
    {
        target = currTarget;
    }

    // function: Start
    // Purpose: Destroy any bullets that last at least 5 seconds flying.
    void Start()
    {
        // Destroys bullets after 5 seconds.
        Destroy(gameObject, 5f);
    }

    // function: Update
    // Purpose: Checks to see if there is a target, if not the return
    //          If there is then we see how close the projectile is to the enemy.
    //          If the magnitude of the target's position is less than the amount of distance traveled in a frame, call HitTarget and return.
    //          Else translate the bullet in the direction of the enemy by distanceThisFrame
    void Update()
    {
        // if there is no target, return until one is given
        if (target == null)
        {
            return;
        }

        
        Vector2 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        // Moves bullet towards target
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    // function: Hit Target
    // Purpose: Upon making contact with the enemy, destroy the projectile
    void HitTarget()
    {
        Destroy(gameObject);
    }
}
