/***************************************************************
*file: FlowField.cs
*author: Ethan Buff
*class: CS 4700 – Game Development
*assignment: program 4
*date last modified: 11/15/2026
*
*purpose: Computes one (1) flow field for enemy pathfinding.
*         Holds the last computed field for future access.         
*
****************************************************************/
using System.Threading.Tasks;

public class FlowField {
    // AI.Direction is just an enum that can be:
    //  none or any of the 8 cardinal + ordinal directions
    private AI.Direction[,] field;
    private int width;
    private int height;
    private bool swims;
    public AI.Direction[,] GetField() { return (AI.Direction[,])field.Clone();}

    public FlowField(int width, int height, bool swims = false)
    {
        this.width = width;
        this.height = height;
        this.swims = swims;
    }


    // function: IntegrateField
    // purpose: Calculates array of costs to reach the goal for Compute to use
    private float[,] IntegrateField(AITileGrid tiles, (int x, int y)[] goals)
    {
        // Initialize the integration array with a default value of infinity
        float[width, height] integration;
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                integration[i, j] = float.PositiveInfinity;
            }
        }

        open = new MinHeap(width*height);

        // Goals have a cost of 0, you're already there. Calcs start from them.
        foreach ((int x, int y) goal in goals)
        {
            integration[goal.x, goal.y] = 0;
            open.Push(goal.y*width + goal.x, 0.0f);
        }

        // Main loop for all costs
        while (open.Count != 0)
        {
            (int cell, float cost) current = open.Pop();
            int x = cell % width;
            int y = cell / width;

            // If a cell's cost is calculated multiple times, only keep the lowest
            if (current.cost > integration[x, y]) continue;


            // Handle every direction one by one because I don't wanna figure out the elegant solution
            // Order: E, NE, N, NW, W, SW, S, SE

            // E (x+1, y)
            if ((AITileGrid[x+1, y].GetWalkable()) && (this.swims || !(AITileGrid[x+1, y].GetIsWater())))
            {
                cost = integration[x, y] + AITileGrid[x+1, y].GetDifficulty();
                if (cost < integration[x+1, y])
                {
                    integration[x+1, y] = cost;
                    open.push((x+1+(y*width), cost));
                }
            }

            // NE (x+1, y+1)
            if ((AITileGrid[x+1, y+1].GetWalkable()) && (this.swims || !(AITileGrid[x+1, y+1].GetIsWater())))
            {
                // Diagonal directions cost more movement (approximately sqrt 2)
                cost = integration[x, y] + AITileGrid[x+1, y+1].GetDifficulty()*1.414;
                if (cost < integration[x+1, y+1])
                {
                    integration[x+1, y+1] = cost;
                    open.push((x+1+((y+1)*width), cost));
                }
            }

            // N (x, y+1)
            if ((AITileGrid[x, y+1].GetWalkable()) && (this.swims || !(AITileGrid[x, y+1].GetIsWater())))
            {
                cost = integration[x, y] + AITileGrid[x, y+1].GetDifficulty();
                if (cost < integration[x, y+1])
                {
                    integration[x, y+1] = cost;
                    open.push((x+((y+1)*width), cost));
                }
            }

            // NW (x-1, y+1)
            if ((AITileGrid[x-1, y+1].GetWalkable()) && (this.swims || !(AITileGrid[x-1, y+1].GetIsWater())))
            {
                cost = integration[x, y] + AITileGrid[x-1, y+1].GetDifficulty()*1.414;
                if (cost < integration[x-1, y+1])
                {
                    integration[x-1, y+1] = cost;
                    open.push((x-1+((y+1)*width), cost));
                }
            }

            // W (x-1, y)
            if ((AITileGrid[x-1, y].GetWalkable()) && (this.swims || !(AITileGrid[x-1, y].GetIsWater())))
            {
                cost = integration[x, y] + AITileGrid[x-1, y].GetDifficulty();
                if (cost < integration[x-1, y])
                {
                    integration[x-1, y] = cost;
                    open.push((x-1+(y*width), cost));
                }
            }

            // SW (x-1, y-1)
            if ((AITileGrid[x-1, y-1].GetWalkable()) && (this.swims || !(AITileGrid[x-1, y-1].GetIsWater())))
            {
                cost = integration[x, y] + AITileGrid[x-1, y-1].GetDifficulty()*1.414;
                if (cost < integration[x-1, y-1])
                {
                    integration[x-1, y-1] = cost;
                    open.push((x-1+((y-1)*width), cost));
                }
            }

            // S (x, y-1)
            if ((AITileGrid[x, y-1].GetWalkable()) && (this.swims || !(AITileGrid[x, y-1].GetIsWater())))
            {
                cost = integration[x, y] + AITileGrid[x, y-1].GetDifficulty();
                if (cost < integration[x, y-1])
                {
                    integration[x, y-1] = cost;
                    open.push((x+((y-1)*width), cost));
                }
            }

            // SE (x+1, y-1)
            if ((AITileGrid[x+1, y-1].GetWalkable()) && (this.swims || !(AITileGrid[x+1, y-1].GetIsWater())))
            {
                cost = integration[x, y] + AITileGrid[x+1, y-1].GetDifficulty()*1.414;
                if (cost < integration[x+1, y-1])
                {
                    integration[x+1, y-1] = cost;
                    open.push((x+1+((y-1)*width), cost));
                }
            }
        }

        return integration;

    }

    // function: Compute
    // purpose: Uses the Dijkstra algorithm reverse-style to create a flow field,
    //          which tells enemies how to traverse.
    //          It updates field and also returns a copy to the caller.
    //          
    public AI.Direction[,] Compute(AITileGrid tiles, (int x, int y)[] goals)
    {
        AI.Direction[width, height] newField;
        float[width,height] costs = IntegrateField(tiles, goals);
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < width; j++)
            {
                if (costs[i, j] == float.PositiveInfinity) // unreachable/impassable
                {
                    newField[i, j] = AI.Direction.None;
                    continue;
                }

                (int x, int y) best = (i, j);

                // I think I figured out the elegant way to do it
                for(int x = -1; x <= 1; x++) {
                    for(int y = -1; y <= 1; y++) {
                        if(x == 0 && y == 0) continue;
                        if(costs[i+x, j+y] < costs[best.x, best.y]) best = (i+x, j+y);
                    }
                newField[i, j] = AI.DirectionHelp.IntsToDir(i - best.x, j - best.y);
                }
            }
        }
        field = newField;
        return newField;
    }

}
