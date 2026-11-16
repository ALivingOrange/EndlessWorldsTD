/***************************************************************
*file: AITile.cs
*author: Ethan Buff
*class: CS 4700 – Game Development
*assignment: program 4
*date last modified: 11/15/2026
*
*purpose: This is a plain CS struct used to store information
*         assigned to individual tiles for AI processing.
*         Represents one tile's attributes.
****************************************************************/
using UnityEngine;

[System.Serializable]
public struct AITile
{
    private bool walkable;
    public bool GetWalkable() {return walkable;}
    public void SetWalkable(bool a) {walkable = a;}

    private bool isWater;
    public bool GetIsWater() {return isWater;}
    public void SetIsWater(bool a) {isWater = a;}
    
    // How hard it is to walk over terrain; different AI may treat it differently.
    // It's intended to be higher difficulty value = slower movement generally.
    // Cannot be less than 1 (1 = no difficulty impact)
    private float difficulty;
    public float GetDifficulty() {return difficulty;}
    public void SetDifficulty(float a) {difficulty = Mathf.Max(a, 1.0f);}
}
