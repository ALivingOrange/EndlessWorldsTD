/***************************************************************
*file: TowerCoreManager.cs
*author: G. Valdiviez
*class: CS 4700 – Game Development
*assignment: Final Program
*date last modified: 11/21/2026
*
*purpose: Allows the Tower Cores to function as intended by the development team
*         Determines the physics attributes of the tower, i.e. damage, fire rate, shots per burst.
*         A TowerCoreManager object can be attached to a TowerBase.
*
****************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Collections.Tests.CoreCLR.TestJobs;
using Unity.VisualScripting;
using UnityEngine;

public class TowerCoreManager : MonoBehaviour
{
    [Header("Core Data")]
    public TowerCoreData coreData;
    protected TowerBaseManager parentBase;

    [Header("Effective Tower Stats")]
    public float currDamage;
    public float currFireInterval;
    public int currShotsPerBurst;
    public float currAOE;
    public DamageType currDamageType;
    private float fireCountdown = 0f;

    // function: Initialize
    // Purpose: We assign parentBase to the base b.
    //          Then we see if we have an attached head w/ data.
    //          If we do then we combine the head's base attack stats with the core's stat modifiers to get our current attack stats.
    public virtual void Initialize(TowerBaseManager b)
    {
        parentBase = b;

        if (coreData == null || parentBase == null)
        {
            return;
        }

        TowerHeadManager head = parentBase.attachedHead;
        if (head == null || head.headData == null)
        {
            return;
        }

        TowerHeadData headStats = head.headData;
        currDamage = headStats.baseDamage * coreData.damageModifier;
        currFireInterval = Mathf.Max(0.01f, headStats.baseFireInterval * coreData.fireIntervalModifier);
        currAOE = headStats.baseAOE * coreData.areaOfEffectModifier;
        currShotsPerBurst = headStats.shotsPerBurst;

        currDamageType = coreData.damageType;
    }

    // function: Update
    // Purpose: Called every frame while the script is active.
    //          Every frame this function does a multitude of things:
    //               - Check to see if there The tower is complete, or if the core has a base
    //               - Checks to see if the tower is able to be fired
    //               - If it is, then it calls FireBurst() and restarts the fireCountdown.
    void Update()
    {
        if (parentBase == null || !parentBase.IsTowerComplete())
        {
            return;
        }

        if (parentBase.attachedHead == null)
        {
            return;
        }

        if (fireCountdown > 0f)
        {
            fireCountdown -= Time.deltaTime;
        }

        if (parentBase.attachedHead.RequiresTarget)
        {
            if (parentBase.Target == null)
            {
                return;
            }

            if (!parentBase.attachedHead.IsFacingTarget())
            {
                return;
            }
        }

        if (fireCountdown <= 0f)
        {
            StartCoroutine(FireBurst());
            fireCountdown = MathF.Max(0.01f, currFireInterval);
        }
    }

    // function: FireBurst()
    // Purpose: uses TowerData.shotsPerBurst to call Shoot that many times within 0.1f second intervals.
    //          If an enemy were to leave the tower's range before the burst finishes, the remaining bullets won't spawn in.
    IEnumerator FireBurst()
    {
        
        for (int i = 0; i < currShotsPerBurst; i++)
        {
            

            if (parentBase == null || !parentBase.IsTowerComplete())
            {
                yield break;
            }

            if (parentBase.attachedHead == null)
            {
                yield break;
            }

            if (parentBase.attachedHead.RequiresTarget)
            {
                if (parentBase.Target == null)
                {
                    yield break;
                }

                if (!parentBase.attachedHead.IsFacingTarget())
                {
                    yield break;
                }
            }
            
            parentBase.attachedHead.Shoot(parentBase.Target, currDamage);
            yield return new WaitForSeconds(0.1f);
        }
    }

}
