using UnityEngine;

public class GridMap : MonoBehaviour
{
    public int width = 64; // in cells
    public int height = 64; // also in cells
    public float cellSize = 1.0f; // in Unity units

    private bool[,] walkable;


    // Gives center of a cell as a vector3 corresponding to position transform
    // (position is relative to parent object (which should be World))
    public Vector3 CellToWorld(int x, int y) {return new Vector3((x + 0.5f) * cellSize, (y + 0.5f) * cellSize, 0);}
    public Vector3 CellToWorld(Vector2Int coords) {return CellToWorld(coords.x, coords.y);}

    // Finds the cell which the provided coordinates local to world lie within
    // Float to int should truncate properly.
    // I believe that due to finite float precision attempting to use this on a border
    //   is unpredictable.
    public Vector2Int WorldToCell(Vector3 coords) {return new Vector2Int(Mathf.FloorToInt(coords.x / cellSize), Mathf.FloorToInt(coords.y / cellSize));}

    // TEMPORARY: Make walkable a checkerboard
    public void Start() {
        walkable = new bool[width, height];
        for(int i = 0; i < width; i++) {
            for(int j = 0; j < height; j++) {
                walkable[i, j] = (i + j) % 2 == 0;
            }
        }
    }

    // Draws an overlay of the Dijkstra grid that shows how the enemy AI is treating the landscape
    // If this doesn't update as it gets more complex then I have failed.
    private void OnDrawGizmos() {
        if (walkable == null) return;
        Gizmos.matrix = transform.localToWorldMatrix;
        for(int i = 0; i < width; i++) {
            for(int j = 0; j < height; j++) {
                // For now, just black or white boxes.
                if (walkable[i, j]) Gizmos.color = Color.white;
                else if (!walkable[i, j]) Gizmos.color = Color.black;
                Gizmos.DrawCube(center: CellToWorld(i, j), size: new Vector3(cellSize, cellSize, 0));
            }
        }
    }
}
