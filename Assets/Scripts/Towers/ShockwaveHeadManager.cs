/***************************************************************
*file: ShockwaveHeadManager.cs
*author: G. Valdiviez
*class: CS 4700 – Game Development
*assignment: Final Program
*date last modified: 11/21/2026
*
*purpose: Allows the Specialty Shockwave Tower Heads to function as intended by the development team
*         Gives the head attacking abilities that make it differ from normal TowerHeadManager objects. Though it derives from the script
*         Calls ShockwavePhysics.cs to damage all nearby enemies.
*         A ShockwaveHeadManager object can be attached to a TowerBase.
*
****************************************************************/
using UnityEngine;

public class ShockwaveHeadManager : TowerHeadManager
{
    // Doesn't require targeting as it's an AOE attack stemming from the middle of the tower base
    public override bool RequiresTarget => false;

    // function: Shoot
    // Purpose: Overridden from the TowerHeadManager version, this creates the shockwave.
    //          Once the script knows the shockwave has all requirements, it calls Initialize()
    public override void Shoot(Transform target, float damage)
    {
        if (parentBase == null || headData == null || headData.projectilePrefab == null)
        {
            Debug.LogWarning("Shockwave Head is missing required references");
            return;
        }

        // Checking to see if enemies are nearby
        Collider2D[] nearbyColliders = Physics2D.OverlapCircleAll(parentBase.transform.position, parentBase.currentRange);
        bool enemyNearby = false;
        foreach (Collider2D collider in nearbyColliders)
        {
            if (collider.GetComponentInParent<EnemyHealth>() != null)
            {
                enemyNearby = true;
                break;
            }
        }
        if (!enemyNearby)
        {
            return;
        }

        // Creating Shockwave
        GameObject shockwaveObject = Instantiate(headData.projectilePrefab, parentBase.transform.position, Quaternion.identity);

        ShockwavePhysics shockwave = shockwaveObject.GetComponent<ShockwavePhysics>();
        if (shockwave == null)
        {
            Debug.LogError("P_Shockwave is missing ShockwavePhysics");
            Destroy(shockwaveObject);
            return;
        }

        shockwave.Initialize(parentBase.attachedCore.currAOE, damage, parentBase.attachedCore.currDamageType);
    }
}
