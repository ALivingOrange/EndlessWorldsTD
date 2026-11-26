/***************************************************************
*file: GridMap.cs
*author: Ethan Buff
*class: CS 4700 – Game Development
*assignment: program 4
*date last modified: 11/15/2026
*
*purpose: This program defines how the map interacts with Unity
*         engine stuff. [Needs a better description]
****************************************************************/
using UnityEngine;
using System.Collections.Generic;

public class GridMap : MonoBehaviour
{

    // These variables are adjusted based on the worldgen data that's received
    private int width;
    private int height;
    private float tileSize;


    // This being serialized is TEMPorary, should be made on world creation
    [SerializeField] private EnemyData[] enemyTypes;
    private Dictionary<string, FlowField> aiFields;

    // Another TEMPorary serialization, the GameManager should prob handle
    // connections between this script and worldgen
    [SerializeField] private WorldGeneration world;

    // AITileGrid has an indexer (allows [x, y]) but it's READ ONLY!
    private AITileGrid grid;

    // function: Awake
    // purpose: Called first thing when an object becomes active (or e.g. the scene loads)
    //          Here we make sure the grid is instantiated as valid.
    void Awake()
    {
        world.Generated += BuildFromWorld;
        grid = new AITileGrid(width, height);

        aiFields = new Dictionary<string, FlowField>();
    }

    // function: CellToWorld
    // purpose: Gives center of a cell as a vector3 corresponding to position transform
    // (position is relative to parent object (which should be World))
    public Vector3 CellToWorld(int x, int y) {
                    return new Vector3((x + 0.5f) * tileSize, (y + 0.5f) * tileSize, 0);}
    public Vector3 CellToWorld(Vector2Int coords) {return CellToWorld(coords.x, coords.y);}

    // function: WorldToCell
    // purpose: Finds the cell which the provided (local to world) coordinates lie within.
    // Float to int should truncate properly, but I believe that due to finite float precision,
    // attempting to use this exactly on a border near tiles is unpredictable.
    public Vector2Int WorldToCell(Vector3 coords) {
        return new Vector2Int(Mathf.FloorToInt(coords.x / tileSize), Mathf.FloorToInt(coords.y / tileSize));}


    public FlowField GetFlowField(string name) { return aiFields[name]; }

    public float DifficultyAt(int x, int y) {

        if(x < width && y < height && x >= 0 && y >= 0) return grid[x, y].GetDifficulty();
        else return float.PositiveInfinity;
    }

    // function: BuildFromWorld
    // purpose: Sets up AI grid and such based on generated world
    private void BuildFromWorld()
    {
        width = world.Columns;
        height = world.Rows;
        tileSize = world.TileSize;
        grid = new AITileGrid(width, height);
        for(int x = 0; x < width; x++) {
            for(int y = 0; y < height; y++) {
                WorldTiles tile = world.GetTile(x, y);
                grid.SetWalkable(x, y, tile.walkable);
                grid.SetIsWater(x, y, tile.isWater);
                grid.SetDifficulty(x, y, tile.difficulty);
            }
        }
        //TEMP: Goal at center
        (int x, int y) goal = (width/2, height/2);
        (int x, int y)[] goals = new (int x, int y)[1];
        goals[0] = goal;
        foreach(EnemyData enemy in enemyTypes)
        {
            aiFields[enemy.name] = new FlowField(width, height, enemy.swims);
            aiFields[enemy.name].Compute(grid, goals);
        }
    }

    // function: OnDrawGizmos
    // purpose: If Gizmos are enabled, draws an overlay of the AITile grid
    // which shows how the enemy AI is treating the landscape
    private void OnDrawGizmos() {
        if (grid == null) return;
        for(int i = 0; i < width; i++) {
            for(int j = 0; j < height; j++) {
                // For now, just black or white boxes.
                if (!grid[i, j].GetWalkable()) Gizmos.color = Color.black;
                else if (grid[i, j].GetIsWater()) Gizmos.color = Color.blue;
                else Gizmos.color = new Color((grid[i, j].GetDifficulty()/3.0f), 0.5f, 0.2f);
                Gizmos.DrawCube(center: CellToWorld(i, j), size: new Vector3(tileSize, tileSize, 0));
                Gizmos.color = Color.red;
            }
        }
    }
}
