/***************************************************************
*file: AITileData.cs
*author: Ethan Buff
*class: CS 4700 – Game Development
*assignment: program 4
*date last modified: 11/14/2026
*
*purpose: This is a plain CS class used to store information
*         assigned to individual tiles for AI processing.
*         Represents one tile's attributes.
****************************************************************/
using UnityEngine;

[System.Serializable]
public class AITile
{
    // Its internal variables are public, but whatever grid uses them should keep 
    // its AITiles private itself.
    public bool walkable;
    public bool is_water;
    
    // How hard it is to walk over terrain; different AI may treat it differently.
    // It's intended to be higher difficulty value = slower movement generally.
    public float difficulty;

    public AITile()
    {
        walkable = true;
        is_water = true;
        difficulty = 1.0f;
    }

}
