/********************************************************************************
file: WorldVisualizer.cs
author: Joshua C. Lo
class: CS4700 - Game Development

assignemnt: Program 3
date last modified: 11/19/2026

purpose: The purpose of this program is to build the map using different colors
to show where water, cliffs, and walkable areas are. 
********************************************************************************/

using UnityEngine;

public class WorldVisualizer : MonoBehaviour
{
    [SerializeField] private WorldGeneration worldGeneration;

    //Different colors to show the terrain generation
    [SerializeField] private Color normalColor = Color.green;
    [SerializeField] private Color roughColor = Color.yellow;
    [SerializeField] private Color waterColor = Color.blue;
    [SerializeField] private Color cliffColor = Color.gray;

    [SerializeField] private float tileHeight = 0.1f;

    private void Reset(){
        worldGeneration = GetComponent<WorldGeneration>();
    }

    //Creates the map in the Scene plane of Unity
    private void OnDrawGizmos(){
        if(worldGeneration == null){    //Stops if WorldGeneration hasn't been assigned to anything
            return;
        }
        if(worldGeneration.Tiles == null){  //Stops if the world hasn't been generated
            return;
        }
        foreach(WorldTiles tile in worldGeneration.Tiles){      //Goes through every tile in the generated world
            Gizmos.color = GetTerrainColor(tile.terrainType);   //Gets a color based on the terrain type of the tile
            //Draws a cube with the corresponding color of the tile
            Vector3 tilePosition = new Vector3(tile.position.x, tile.position.y, 0f);
            Vector3 tileSize = new Vector3(worldGeneration.tileSize, worldGeneration.tileSize, tileHeight);
            Gizmos.DrawCube(tilePosition, tileSize);
        }
    }

    //Returns the color of the tile based on its terrain type
    private Color GetTerrainColor(TerrainType terrainType){
        switch(terrainType){
            case TerrainType.Normal:
                return normalColor;
            case TerrainType.Rough:
                return roughColor;
            case TerrainType.Water:
                return waterColor;
            case TerrainType.Cliff:
                return cliffColor;
            default:
                return Color.white;
        }
    }
}
