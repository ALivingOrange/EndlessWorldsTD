
/***************************************************************
* file: TowerPartData.cs
* author: Dylan Diaz
* class: CS 4700 - Game Development
* assignment: Endless Worlds TD
* date last modified: 11/25/2026
*
* purpose: Parent class for all tower part data.
* Stores shared display names and crafting costs
* for tower bases, cores, and heads.
*
****************************************************************/

using UnityEngine;

public abstract class TowerPartData : ScriptableObject
{
    [Header("Crafting Information")]

    [SerializeField]
    private string displayName;

    [SerializeField]
    private ResourceCost[] craftingCosts;

    // function: getDisplayName
    // purpose: Returns the player-facing tower part name.
    public string getDisplayName()
    {
        return string.IsNullOrWhiteSpace(displayName)
            ? name
            : displayName;
    }

    // function: getCraftingCosts
    // purpose: Returns the resources required to craft this part.
    public ResourceCost[] getCraftingCosts()
    {
        return craftingCosts;
    }
}