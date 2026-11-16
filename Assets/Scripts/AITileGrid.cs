/***************************************************************
*file: AITileGrid.cs
*author: (FILL THIS IN!)
*class: CS 4700 – Game Development
*assignment: program 4
*date last modified: 11/15/2026
*
*purpose: Plain C# class for containing a whole grid of AITiles
*
****************************************************************/
public class AITileGrid
{
    private AITile[,] grid;
    private int width;
    private int height;

    // function: AITileGrid Constructor
    // purpose: Creates a grid according to provided parameters
    public AITileGrid(int width, int height)
    {
        this.width = width;
        this.height = height;
        grid = new AITile[width, height];
        for(int i = 0; i < width; i++){
            for(int j = 0; j < height; j++)
            {
                grid[i, j].SetWalkable(true);
                grid[i, j].SetIsWater(false);
                grid[i, j].SetDifficulty(1.0f);
            }
        }
    }

    // function: Indexer
    // purpose: READ-ONLY allows a tile to be accessed with [x, y] index
    //          as if AITileGrid was just an array.
    public AITile this[int x, int y]
    {
        get { return grid[x, y];}
    }

    // function: Set{Variable}
    // purpose: Simple setters for tiles
    public void SetWalkable(int x, int y, bool w) { grid[x, y].SetWalkable(w);}
    public void SetIsWater(int x, int y, bool isW) { grid[x, y].SetIsWater(isW);}
    public void SetDifficulty(int x, int y, float d) { grid[x, y].SetDifficulty(d);}
    
}
