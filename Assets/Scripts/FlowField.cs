/***************************************************************
*file: FlowField.cs
*author: Ethan Buff
*class: CS 4700 – Game Development
*assignment: program 4
*date last modified: 11/16/2026
*
*purpose: Computes one (1) flow field for enemy pathfinding.
*         Holds the last computed field for future access.
*
****************************************************************/
using System.Collections.Generic;
// Aliases for readabliity
using static AI.DirectionHelp;

public class FlowField {
    // AI.Direction is just an enum that can be:
    //  none or any of the 8 cardinal + ordinal directions
    private AI.Direction[,] field;
    private int width;
    private int height;
    private bool swims;
    // C# clone of enum[] is a copy of values, shouldn't permit modifying original
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
        float[,] integration = new float[width, height];
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                integration[i, j] = float.PositiveInfinity;
            }
        }

        MinHeap open = new MinHeap(width*height);

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
            (int x, int y) curCoords = (current.cell % width, current.cell/width);

            // If a cell's cost is calculated multiple times, only keep the lowest
            if (current.cost > integration[curCoords.x, curCoords.y]) continue;

            // First we check cardinals, track what's impassable
            bool[] passableCards = { false, false, false, false};
            for(int i = 0; i < 4; i++ )
            {
                AI.Direction dir = CARDINAL_DIRS[i];
                (int x, int y) offset = DirToInts(dir);


                (int x, int y) neighborCoords = (curCoords.x + offset.x, curCoords.y + offset.y);
                // skip if out of bounds
                if(neighborCoords.x >= width || neighborCoords.y >= height) continue;
                if(neighborCoords.x < 0 || neighborCoords.y < 0) continue;

                // writes to this tile affect copy, not original
                AITile neighbor = tiles[neighborCoords.x, neighborCoords.y];

                // Passable check: Walkable and must be able to swim if tile is water
                if(neighbor.GetWalkable() && (!neighbor.GetIsWater() || this.swims))
                {
                    passableCards[i] = true;

                    float cost = integration[curCoords.x, curCoords.y] + neighbor.GetDifficulty();
                    if(cost < integration[neighborCoords.x, neighborCoords.y]) {
                        integration[neighborCoords.x, neighborCoords.y] = cost;
                        open.Push(neighborCoords.x + (neighborCoords.y*width), cost);
                    }
                }
            }

            // Now check ordinals, but they're skipped if both adjacent cardinals are impassable
            // (because the enemy shouldn't take that path)
            // This can be changed to either being impassable if enemies cut weird corners.
            for(int i = 0; i < 4; i++ )
            {
                AI.Direction dir = ORDINAL_DIRS[i];
                (int x, int y) offset = DirToInts(dir);


                (int x, int y) neighborCoords = (curCoords.x + offset.x, curCoords.y + offset.y);
                // skip if out of bounds
                if(neighborCoords.x >= width || neighborCoords.y >= height) continue;
                if(neighborCoords.x < 0 || neighborCoords.y < 0) continue;
                // skip if prev (at i) and next (i + 1, % 4 for the wraparound) cardinals impassable
                if(!passableCards[i] && !passableCards[(i+1) % 4]) continue;

                // writes to this tile affect copy, not original
                AITile neighbor = tiles[neighborCoords.x, neighborCoords.y];

                // Passable check: Walkable and must be able to swim if tile is water
                if(neighbor.GetWalkable() && (!neighbor.GetIsWater() || this.swims))
                {
                    float cost = integration[curCoords.x, curCoords.y] + neighbor.GetDifficulty();
                    if(cost < integration[neighborCoords.x, neighborCoords.y]) {
                        integration[neighborCoords.x, neighborCoords.y] = cost;
                        open.Push(neighborCoords.x + (neighborCoords.y*width), cost);
                    }
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
        AI.Direction[,] newField = new AI.Direction[width, height];
        float[,] costs = IntegrateField(tiles, goals);
        for(int i = 0; i < width; i++) {
            for(int j = 0; j < height; j++) {
                if (costs[i, j] == float.PositiveInfinity) // unreachable/impassable
                {
                    newField[i, j] = AI.Direction.None;
                    continue;
                }

                (int x, int y) best = (i, j);

                // Unsure if this or the one using more Direction stuff in Integrate is better
                for(int x = -1; x <= 1; x++) {
                    for(int y = -1; y <= 1; y++) {
                        if(x == 0 && y == 0) continue;
                        if(costs[i+x, j+y] < costs[best.x, best.y]) best = (i+x, j+y);
                    }
                }
                newField[i, j] = IntsToDir(best.x - i, best.y - j);
            }
        }
        field = newField;
        return newField;
    }

}
