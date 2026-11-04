/***************************************************************
*file: GridMap.cs
*author: E. Buff
*class: CS 4700 – Game Development
*assignment: program 4
*date last modified: 11/4/2026
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

    private bool[,] walkable;

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
    // TEMPORARY: Make walkable a checkerboard
    public void Start() {
        walkable = new bool[WIDTH, HEIGHT];
        for(int i = 0; i < WIDTH; i++) {
            for(int j = 0; j < HEIGHT; j++) {
                walkable[i, j] = (i + j) % 2 == 0;
            }
        }
    }

    // function: OnDrawGizmos
    // purpose: If Gizmos are enabled, draws an overlay of the Dijkstra grid 
    // which shows how the enemy AI is treating the landscape
    private void OnDrawGizmos() {
        if (walkable == null) return;
        Gizmos.matrix = transform.localToWorldMatrix;
        for(int i = 0; i < WIDTH; i++) {
            for(int j = 0; j < HEIGHT; j++) {
                // For now, just black or white boxes.
                if (walkable[i, j]) Gizmos.color = Color.white;
                else if (!walkable[i, j]) Gizmos.color = Color.black;
                Gizmos.DrawCube(center: CellToWorld(i, j), size: new Vector3(CELL_SIZE, CELL_SIZE, 0));
            }
        }
    }
}
