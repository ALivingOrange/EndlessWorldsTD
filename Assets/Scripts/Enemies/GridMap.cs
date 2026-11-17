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

public class GridMap : MonoBehaviour
{
    [SerializeField] private int DEFAULT_WIDTH = 64; // in cells
    [SerializeField] private int DEFAULT_HEIGHT = 64; // also in cells
    [SerializeField] private float CELL_SIZE = 1.0f; // in Unity units

    // Separating default width/height and current is just in case, maybe silly
    // if they end up never changing.
    private int width;
    private int height;

    // AITileGrid has an indexer (allows [x, y]) but it's READ ONLY!
    private AITileGrid grid;

    // TEMP variable: enemy AIs will have their own flow field objects later
    private FlowField aiFieldCalc;
    private AI.Direction[,] aiField;

    // function: Awake
    // purpose: Called first thing when an object becomes active (or e.g. the scene loads)
    //          Here we make sure the grid is instantiated as valid.
    void Awake()
    {
        grid = new AITileGrid(DEFAULT_WIDTH, DEFAULT_HEIGHT);
        width = DEFAULT_WIDTH;
        height = DEFAULT_HEIGHT;
        
        // TEMP: Random map for dijkstra tests
        Random.InitState(42); // seeded for reproduction
        for(int i = 0; i < width; i++) {
            for(int j = 0; j < height; j++) {
                if ((j > 50) || (j < 14)) grid.SetWalkable(i, j, false);
                else if ((i > 50) || (i < 14)) grid.SetIsWater(i, j, true);
                else grid.SetDifficulty(i, j, Random.Range(1.0f, 3.0f));
            }
        }

        aiFieldCalc = new FlowField(width, height);
        (int x, int y) goal1 = (32, 32);
        (int x, int y)[] goals = new (int x, int y)[1];
        goals[0] = goal1;
        aiField = aiFieldCalc.Compute(grid, goals);
    }

    // function: CellToWorld
    // purpose: Gives center of a cell as a vector3 corresponding to position transform
    // (position is relative to parent object (which should be World))
    public Vector3 CellToWorld(int x, int y) {
                    return new Vector3((x + 0.5f) * CELL_SIZE, (y + 0.5f) * CELL_SIZE, 0);}
    public Vector3 CellToWorld(Vector2Int coords) {return CellToWorld(coords.x, coords.y);}

    // function: WorldToCell
    // purpose: Finds the cell which the provided (local to world) coordinates lie within.
    // Float to int should truncate properly, but I believe that due to finite float precision,
    // attempting to use this exactly on a border near tiles is unpredictable.
    public Vector2Int WorldToCell(Vector3 coords) {
        return new Vector2Int(Mathf.FloorToInt(coords.x / CELL_SIZE), Mathf.FloorToInt(coords.y / CELL_SIZE));}

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
                Gizmos.DrawCube(center: CellToWorld(i, j), size: new Vector3(CELL_SIZE, CELL_SIZE, 0));
                Gizmos.color = Color.red;
            }
        }
        Gizmos.color = Color.red;
        for(int i = 0; i < width; i++) {
            for(int j = 0; j < height; j++) {
                (int x, int y) directionIntTuple = AI.DirectionHelp.DirToInts(aiField[i, j]);
                Vector3 direction = new Vector3((float)directionIntTuple.x, (float)directionIntTuple.y, 0);
                Gizmos.DrawRay(CellToWorld(i, j), direction);
            }
        }
    }
}
