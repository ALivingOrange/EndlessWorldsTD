/***************************************************************
*file: TowerData.cs
*author: G. Valdiviez
*class: CS 4700 – Game Development
*assignment: Final Program
*date last modified: 11/1/2026
*
*purpose: Holds the data used in the TowerManager.cs script
*         Because it is a separate file, any single shot towers can have unique stats without too much alteration
*         
*
****************************************************************/

using UnityEngine;

[CreateAssetMenu(fileName = "New Tower Data", menuName = "EndlessWorldsTD Tower Data")]
public class TowerData : ScriptableObject
{
    [Header("Attributes")]
    public float baseDamage;
    public float attackRange;
    public float fireRate;

    [Header("Unity Setup Fields")]
    public float turnSpeed;
    public string enemyTag;

    [Header("Bullet Components")]
    public GameObject projectilePrefab;
}
