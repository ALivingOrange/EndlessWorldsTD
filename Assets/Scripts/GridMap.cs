/***************************************************************
*file: GridMap.cs
*author: E. Buff
*class: CS 4700 – Game Development
*assignment: program 4
*date last modified: 11/14/2026
*
*purpose: This program defines how enemy AI sees the map,
*         containing all information necessary for pathfinding.
****************************************************************/
using UnityEngine;

public class GridMap : MonoBehaviour
{
    [SerializeField] private int WIDTH = 64; // in cells
    [SerializeField] private int HEIGHT = 64; // also in cells
    [SerializeField] private float CELL_SIZE = 1.0f; // in Unity units

    private AITile[,] grid;

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

    // function: Start
    // purpose: Called on the frame when the script is enabled. Here¸ it initializes the grid's arrray.
    // TEMPORARY: Assign a bunch of values randomly
    public void Start() {
        grid = new AITile[WIDTH, HEIGHT];
        for(int i = 0; i < WIDTH; i++) {
            for(int j = 0; j < HEIGHT; j++) {
                grid[i, j] = new AITile();
                grid[i, j].walkable = Random.value > 0.25f;
                grid[i, j].is_water = Random.value < 0.25f;
                grid[i, j].difficulty = Random.Range(0.0f, 3.0f);
            }
        }
    }

    // function: OnDrawGizmos
    // purpose: If Gizmos are enabled, draws an overlay of the Dijkstra grid 
    // which shows how the enemy AI is treating the landscape
    private void OnDrawGizmos() {
        if (grid == null) return;
        Gizmos.matrix = transform.localToWorldMatrix;
        for(int i = 0; i < WIDTH; i++) {
            for(int j = 0; j < HEIGHT; j++) {
                // For now, just black or white boxes.
                if (!grid[i, j].walkable) Gizmos.color = Color.black;
                else if (grid[i, j].is_water) Gizmos.color = Color.blue;
                else Gizmos.color = new Color(Mathf.Min(1.0f, grid[i, j].difficulty/3.0f), 0.5f, 0.2f);
                Gizmos.DrawCube(center: CellToWorld(i, j), size: new Vector3(CELL_SIZE, CELL_SIZE, 0));
            }
        }
    }
}
