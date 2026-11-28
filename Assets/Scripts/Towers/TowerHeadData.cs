/***************************************************************
*file: TowerBaseData.cs
*author: G. Valdiviez
*class: CS 4700 – Game Development
*assignment: Final Program
*date last modified: 11/21/2026
*
*purpose: Holds the data used in the TowerHeadManager.cs script
*         Because it is a separate file, any towers can have unique stats without too much alteration
*         
*
****************************************************************/
using UnityEngine;

[CreateAssetMenu(fileName = "TowerHeadData", menuName = "Scriptable Objects/TowerHeadData")]
public class TowerHeadData : TowerPartData
{
    [Header("Attributes")]
    public GameObject projectilePrefab;
    public int numOfProjectiles; // This is per shot, not per burst.
    public int shotsPerBurst = 1;

    [Header("Base Attack Stats")]
    public float baseDamage;
    public float baseFireInterval;
    public float baseAOE;

    [Header("Random Projectile Settings")]
    public bool randomizeProjectileCount;
    public int minProjectiles = 3;
    public int maxProjectiles = 6;

    [Header("Projectile Spread")]
    public float spreadAngle = 60f;

    [Header("Unity Setup Fields")]
    public float turnSpeed;
    public GameObject headPrefab;

}
