/********************************************************************************
file: WorldTiles.cs
author: Joshua C. Lo
class: CS4700 - Game Development

assignemnt: Program 3
date last modified: 11/19/2026

purpose: The purpose of this program is to determine what each individual map tile
allows. For example, if the tile if walkable or impassible.
********************************************************************************/

using UnityEngine;

public enum TerrainType{
    Normal, Rough, Water, Cliff
}

public class WorldTiles
{
    public Vector2 position;
    public TerrainType terrainType;
    public bool walkable;
    public bool isWater;
    public float difficulty;

    //Creates a tile at the position
    public WorldTiles(Vector2 position){
        this.position = position;
        setTerrain(TerrainType.Normal);     //Assumes all tiles are initially normal
    }

    //Changes the terrain and updates the terrain tile to have the properties of what the tile is
    public void setTerrain(TerrainType type){
        terrainType = type;

        //Changes the terrain based on the tile
        switch(type){
            case TerrainType.Normal:
                walkable = true;
                isWater = false;
                difficulty = 1.0f;
                break;
            case TerrainType.Rough:
                walkable = true;
                isWater = false;
                difficulty = 1.5f;
                break;
            case TerrainType.Water:
                walkable = false;
                isWater = true;
                difficulty = 3.0f;
                break;
            case TerrainType.Cliff:
                walkable = false;
                isWater = false;
                difficulty = 3.0f;
                break;
        }
    }
}
