/********************************************************************************
file: WorldGeneration.cs
author: Joshua C. Lo
class: CS4700 - Game Development

assignemnt: Program 3
date last modified: 11/19/2026

purpose: The purpose of this program is to create the map and assign each
world tile a terrain type while also checking neighboring tiles to ensure world
smoothing to make sure the world generation makes sense.
********************************************************************************/

using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    public event System.Action Generated;


    public WorldBounds WorldBounds = new WorldBounds(500f, 500f);
    public float tileSize = 5f;

    //Controls how the map is generated
    public int seed = 1111111;
    public bool randomSeed = true;     //Choses a new seed when a new map is generated

    private float noiseScale = 0.08f;   //The size of Perlin Noise patterns
    public float waterGeneration = 0.30f;
    public float roughGeneration = 0.58f;
    public float cliffGeneration = 0.78f;
    public int smoothing = 2;   //Checks the generation of the world to make sure its coherent

    private List<WorldTiles> tiles = new List<WorldTiles>();     //Stores the individual cells of the generated world so we can access them
    private WorldTiles[,] tileGrid;
    private int columns;
    private int rows;
    public int Columns => columns;
    public int Rows => rows;
    public IReadOnlyList<WorldTiles> Tiles => tiles;
    public float TileSize => tileSize;

    //Generates a world when the game starts
    private void Start(){
        GenerateWorld();
    }

    //Handles the world generation
    private void GenerateWorld(){
        if(tileSize <= 0){
            Debug.Log("Tile size has to be greater than 0");
            return;
        }
        if(randomSeed){     //Randomizes a new seed if randomized seed is set to true
            seed = new System.Random().Next();
        }
        System.Random random = new System.Random(seed);     //Generates a random number for the seed

        float randomXoffset = (float)random.NextDouble() * 10000f;
        float randomYoffset = (float)random.NextDouble() * 10000f;

        CreateGrid();      //Creates an empty grid for the world to store tiles
        GenerateTerrain(randomXoffset, randomYoffset);

        for(int i = 0; i < smoothing; i++){     //Makes the terrain look more natural so there aren't random water/cliff tiles in the middle of no where
            TerrainSmoothing();
        }
        Debug.Log("Map has successfully generated!");   //Message that prints out to ensure it works
        Debug.Log("Total tiles generated: " + tiles.Count);

        Generated?.Invoke();
    }

    private void CreateGrid(){
        tiles.Clear();
        //Gets the amount of tiles that can fit in the world
        columns = Mathf.CeilToInt(WorldBounds.width / tileSize);
        rows = Mathf.CeilToInt(WorldBounds.height / tileSize);
        tileGrid = new WorldTiles[columns, rows];
        
        Vector2 worldMin = WorldBounds.Min;
        //Creates every tile in the world
        for(int x = 0; x < columns; x++){
            for(int y = 0; y < rows; y++){
                //Gets the center position of each tile
                Vector2 position = new Vector2(worldMin.x + (x + 0.5f) * tileSize, worldMin.y + (y + 0.5f) * tileSize);
                WorldTiles newTile = new WorldTiles(position);  //Creates a new tile
                tileGrid[x,y] = newTile;    //Stores the tile in the grid
                tiles.Add(newTile);     //Adds the tile to the list of tiles in the world
            }
        }
    }

    //Generates the terrain for the world
    private void GenerateTerrain(float randomXoffset, float randomYoffset){
        //Goes through ever tile generated for the world
        for(int x = 0; x < columns; x++){
            for(int y = 0; y < rows; y++){
                //Calculates the perlin noise within that tile
                float noise = Mathf.PerlinNoise((x * noiseScale) + randomXoffset, (y * noiseScale) + randomYoffset);
                //Gets the current tile
                WorldTiles tile = tileGrid[x,y];
                //Depending on the value of the perlin noise value, it assigns a terrain
                if(noise < waterGeneration){
                    tile.setTerrain(TerrainType.Water);
                }
                else if(noise > cliffGeneration){
                    tile.setTerrain(TerrainType.Cliff);
                }
                else if(noise > roughGeneration){
                    tile.setTerrain(TerrainType.Rough);
                }
                else{
                    tile.setTerrain(TerrainType.Normal);
                }
            }
        }
    }

    //Handles terrain smoothing which makes the world generation more coherent
    private void TerrainSmoothing(){
        //Saves the current terrain tile so it doesn't change any nearby tiles
        TerrainType[,] oldTerrain = new TerrainType[columns, rows];

        for(int x = 0; x < columns; x++){
            for(int y = 0; y < rows; y++){
                oldTerrain[x,y] = tileGrid[x,y].terrainType;
            }
        }
        for(int x = 0; x < columns; x++){
            for(int y = 0; y < rows; y++){
                int[] neighborTerrain = new int[4]; //Checks the terrain type of the neighboring tiles
                
                //Checks the surrounding tiles
                for(int offsetX = -1; offsetX <= 1; offsetX++){
                    for(int offsetY = -1; offsetY <= 1; offsetY++){
                        if(offsetX == 0 && offsetY == 0){   //Makes sure it doesn't count itself as a surrounding tile
                            continue;
                        }
                        int neighborX = x + offsetX;
                        int neighborY = y + offsetY;
                        //Ensures the neighbor tile is within the world bounds
                        if(neighborX >= 0 && neighborX < columns && neighborY >= 0 && neighborY < rows){
                            //Counts how many terrain types there are around the tile
                            int terrainNum = (int)oldTerrain[neighborX, neighborY];
                            neighborTerrain[terrainNum]++;
                        }
                    }
                }
                int highestCount = 0;   //Finds the most common terrain type around the tile
                int mostCommonTerrain = (int)oldTerrain[x,y];
                for(int i = 0; i < neighborTerrain.Length; i++){
                    if(neighborTerrain[i] > highestCount){
                        highestCount = neighborTerrain[i];
                        mostCommonTerrain = i;
                    }
                }
                //Changes tile to the corresponding tiles if at least 5 neighboring tiles share a terrain type
                if(highestCount >= 5){
                    tileGrid[x,y].setTerrain((TerrainType)mostCommonTerrain);
                }
            }

        }

    }

    //Used to find world tiles at a given coordinate inside the tile grid
    public WorldTiles GetTile(int x, int y){
        if(x < 0 || x >= columns || y < 0 || y >= rows){
            return null;
        }
        return tileGrid[x,y];
    }

    //Used to find world tiles based on its position in the world
    public WorldTiles GetTileAt(Vector2 worldPosition){
        Vector2 worldMin = WorldBounds.Min;

        int x = Mathf.FloorToInt((worldPosition.x - worldMin.x) / tileSize);
        int y = Mathf.FloorToInt((worldPosition.y - worldMin.y) / tileSize);

        return GetTile(x,y);
    }
}
