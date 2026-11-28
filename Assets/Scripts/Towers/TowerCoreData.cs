/***************************************************************
*file: TowerBaseData.cs
*author: G. Valdiviez
*class: CS 4700 – Game Development
*assignment: Final Program
*date last modified: 11/21/2026
*
*purpose: Holds the data used in the TowerCoreManager.cs script
*         Because it is a separate file, any towers can have unique stats without too much alteration
*         
*
****************************************************************/
using UnityEngine;

[CreateAssetMenu(fileName = "TowerCoreData", menuName = "Scriptable Objects/TowerCoreData")]
public class TowerCoreData : TowerPartData
{
    [Header("Core Stat Modifiers")]
    public float damageModifier = 1f;
    public float fireIntervalModifier = 1f;
    public float areaOfEffectModifier = 1f;

    [Header("Elemental Damage")]
    public DamageType damageType = DamageType.Normal;

    [Header("Unity Setup Field")]
    public GameObject corePrefab;
}
