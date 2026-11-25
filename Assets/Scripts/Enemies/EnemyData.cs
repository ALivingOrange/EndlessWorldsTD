/***************************************************************
*file: EnemyData.cs
*author: Ethan Buff
*class: CS 4700 – Game Development
*assignment: program 4
*date last modified: 11/24/2026
*
*purpose: ScriptableObject definition for enemies. Everything
*         that makes an enemy unique should be defined here such
*         that 0 code is necessary for making new enemies
****************************************************************/
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyBaseData", menuName = "EndlessWorldsTD Enemy Base Data")]
public class EnemyData : ScriptableObject
{
    public string name;
    public bool swims;
}
