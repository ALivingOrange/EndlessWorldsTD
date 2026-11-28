/********************************************************************************
file: WorldGeneration.cs
author: Joshua C. Lo
class: CS4700 - Game Development

assignemnt: Program 3
date last modified: 11/24/2026

purpose: The purpose of this program is to create the map and assign each
world tile a terrain type while also checking neighboring tiles to ensure world
smoothing to make sure the world generation makes sense.
********************************************************************************/

using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    [SerializeField] private WorldGenerationSettings settings;

    private List<WorldTiles> tiles = new List<WorldTiles>();
    private WorldTiles[,] tileGrid;
    private int columns;
    private int rows;
    public IReadOnlyList<WorldTiles> Tiles => tiles;
    public float tileSize => settings != null ? settings.tileSize : 0f;

    //Game manager should be able to call this function hopefully
    public bool GenerateWorld(){
        //Makes sure that world generation settings exist
        if(settings == null){
            Debug.Log("World Generation Settings have not been assigned");
            return false;
        }
        //Makes sure tile size is valid
        if(settings.tileSize <= 0){
            Debug.Log("Tile size has to be greater than 0");
            return false;
        }   
        //Generates the world several times depending on if it's a good seed or not
        for(int attempt = 0; attempt < settings.maxGen; attempt++){
            Debug.Log("Generating World, Attempt No.: " + (attempt + 1));
            int worldSeed;

            if(settings.randomSeed){
                worldSeed = Random.Range(0, int.MaxValue);
            }
            else{
                worldSeed = settings.seed;  //Uses seed set in the settings
            }
            System.Random random = new System.Random(worldSeed);
            //Random offsets for perlin noise
            float randomXoffset = (float)random.NextDouble() * 10000f;
            float randomYoffset = (float)random.NextDouble() * 10000f; 

            CreateGrid();   //Creates an empty grid for the world
            GenerateTerrain(randomXoffset, randomYoffset); //Generates the terrain for the world

            //Smooths terrain
            for(int i = 0; i < settings.smoothing; i++){
                TerrainSmoothing();
            }
            //Checks to make sure if the map has good world generation
            if(CheckMap()){
                Debug.Log("Map has been successfully generated");
                Debug.Log("Total tiles generated: " + tiles.Count);
                ResourcePoints();   //Places resource points on the map

                return true;
            }
            //The map sucked, so it will generate another map
            Debug.Log("Map has too many unreachable areas. Generating a new map.");
        }
        //All 10 generated maps suck 
        Debug.Log("Could not generate a valid map.");
        return false;
    }

    private void CreateGrid(){
        tiles.Clear();
        //Gets the amount of tiles that can fit in the world
        columns = Mathf.CeilToInt(settings.worldWidth / settings.tileSize);
        rows = Mathf.CeilToInt(settings.worldHeight / settings.tileSize);
        tileGrid = new WorldTiles[columns, rows];
        
        Vector2 worldMin = new Vector2(-settings.worldWidth / 2f, -settings.worldHeight / 2f);
        //Creates every tile in the world
        for(int x = 0; x < columns; x++){
            for(int y = 0; y < rows; y++){
                //Gets the center position of each tile
                Vector2 position = new Vector2(worldMin.x + (x + 0.5f) * settings.tileSize, worldMin.y + (y + 0.5f) * settings.tileSize);
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
                float noise = Mathf.PerlinNoise((x * settings.noiseScale) + randomXoffset, (y * settings.noiseScale) + randomYoffset);
                //Gets the current tile
                WorldTiles tile = tileGrid[x,y];
                //Depending on the value of the perlin noise value, it assigns a terrain
                if(noise < settings.waterGeneration){
                    tile.setTerrain(TerrainType.Water);
                }
                else if(noise > settings.cliffGeneration){
                    tile.setTerrain(TerrainType.Cliff);
                }
                else if(noise > settings.roughGeneration){
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

    private bool CheckMap(){
        int buildableTiles = 0;
        int walkableTiles = 0;
        foreach(WorldTiles tile in tiles){
            if(tile.terrainType == TerrainType.Normal || tile.terrainType == TerrainType.Rough){
                buildableTiles++;
            }
            if(tile.walkable){
                walkableTiles++;
            }
        }
        if(tiles.Count == 0){
            return false;
        }
        
        float buildablePercent = (float)buildableTiles / tiles.Count;
        float walkablePercent = (float)walkableTiles / tiles.Count;

        Debug.Log("Buildable tiles: " + (buildablePercent * 100f) + "%");
        Debug.Log("Enemy Walkable tiles: " + (walkablePercent * 100f) + "%");

        if(buildablePercent < settings.minBuildablePercent){
            Debug.Log("There are not enough buildable tiles");
            return false;
        }
        if(walkablePercent < settings.minWalkablePercent){
            Debug.Log("There are not enough walkable tiles");
            return false;
        }
        
        int unreachableTiles = findImpassableTiles();

        if(walkableTiles == 0){
            return false;
        }

        float impassablePercent = (float)unreachableTiles / walkableTiles;
        Debug.Log("Impassable walkable tiles: " + (impassablePercent * 100f)+ "%");

        if(impassablePercent > settings.maxImpassablePercent){
            Debug.Log("There is too much impassable terrain");
            return false;
        }

        return true;
    }

    private int findImpassableTiles(){
        WorldTiles startingTile = null;
        foreach(WorldTiles tile in tiles){
            if(tile.walkable){
                startingTile = tile;
                break;
            }
        }

        if(startingTile == null){
            return 0;
        }

        int startX = -1;
        int startY = -1;

        for(int x = 0; x < columns; x++){
            for(int y = 0; y < rows; y++){
                if(tileGrid[x,y] == startingTile){
                    startX = x;
                    startY = y;
                    break;
                }
            }
            if(startX != -1){
                break;
            }
        }
        bool[,] visited = new bool[columns, rows];
        Queue<Vector2Int> tilesToCheck = new Queue<Vector2Int>();
        tilesToCheck.Enqueue(new Vector2Int(startX, startY));
        visited[startX, startY] = true;

        int[] directionX = {1, -1, 0, 0};
        int[] directionY = {0, 0, 1, -1};

        while(tilesToCheck.Count > 0){
            Vector2Int current = tilesToCheck.Dequeue();
            for(int i = 0; i < 4; i++){
                int nextX = current.x + directionX[i];
                int nextY = current.y + directionY[i];
                if(nextX < 0 || nextX >= columns || nextY < 0 || nextY >= rows){
                    continue;
                }
                if(visited[nextX, nextY]){
                    continue;
                }
                if(!tileGrid[nextX, nextY].walkable){
                    continue;
                }
                visited[nextX, nextY] = true;
                tilesToCheck.Enqueue(new Vector2Int(nextX, nextY));
            }
        }
        int impassableTiles = 0;

        for(int x = 0; x < columns; x++){
            for(int y = 0; y < rows; y++){
                if(tileGrid[x,y].walkable && !visited[x,y]){
                    impassableTiles++;
                }
            }
        }
        return impassableTiles;
    }

    private void ResourcePoints(){
        int resourcesPlaced = 0;
        List<WorldTiles> possibleTiles = new List<WorldTiles>();
        foreach(WorldTiles tile in tiles){
            if(tile.walkable){
                possibleTiles.Add(tile);
            }
        }
        while(resourcesPlaced < settings.resources && possibleTiles.Count > 0){
            int randomIndex = Random.Range(0, possibleTiles.Count);
            WorldTiles selectedTile = possibleTiles[randomIndex];
            selectedTile.AddResource();
            possibleTiles.RemoveAt(randomIndex);
            resourcesPlaced++;  
        }
        Debug.Log("Resources points placed: " + resourcesPlaced);
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
        Vector2 worldMin = new Vector2(-settings.worldWidth / 2f, -settings.worldHeight / 2f);

        int x = Mathf.FloorToInt((worldPosition.x - worldMin.x) / settings.tileSize);
        int y = Mathf.FloorToInt((worldPosition.y - worldMin.y) / settings.tileSize);

        return GetTile(x,y);
    }
}
