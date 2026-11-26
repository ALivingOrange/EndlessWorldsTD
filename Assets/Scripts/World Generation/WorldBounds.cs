/********************************************************************************
file: WorldBounds.cs
author: Joshua C. Lo
class: CS4700 - Game Development

assignemnt: Program 3
date last modified: 11/19/2026

purpose: The purpose of this program is to set the boundaries for the map
so the enemies or player can't go outside of the map while also setting the width
and height of the map.
********************************************************************************/

using UnityEngine;

[System.Serializable]
public class WorldBounds
{
    public float width;
    public float height;

    public WorldBounds(float width, float height){
        this.width = width;
        this.height = height;
    }

    //Gets the bottom left corner of the map
    // TEMP: From Ethan. I'm making this 0 to agree with AI stuff
    //       Josh should be redoing things to work ith 0 later
    public Vector2 Min{
        get{
            return new Vector2(0.0f, 0.0f);
        }
    }

    //Gets the top right corner of the map
    // TEMP: From Ethan. I'm changing this to agree with AI stuff
    //       Josh should be redoing things to work ith 0 later
    public Vector2 Max{
        get{
            return new Vector2(width, height);
        }
    }

    //Checking to see if anything lies outside of the map bounds
    public bool Contains(Vector2 position){
        return position.x >= Min.x && position.x <= Max.x && position.y >= Min.y && position.y <= Max.y;
    }
}
