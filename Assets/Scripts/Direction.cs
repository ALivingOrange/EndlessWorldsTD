/***************************************************************
*file: Direction.cs
*author: Ethan Buff
*class: CS 4700 – Game Development
*assignment: program 4
*date last modified: 11/15/2026
*
*purpose: It's just an enum that's used for AI calculations
*         e.g. in FlowField.cs
*
****************************************************************/
using System;

namespace AI
{
    // Starts E and goes counter clockwise Unit Circle math style
    public enum Direction
    { None, E, NE, N, NW, W, SW, S, SE}

    public static class DirectionHelp 
    {
        // Function: IntsToDir
        // Purpose: for taking two ints and converting them to a direction 
        // based on signs only
        public static Direction IntsToDir(int x, int y)
        {
            int a = Math.Sign(x);
            int b = Math.Sign(y);

            return (a, b) switch
            {
                ( 0,  0) => Direction.None,
                ( 1,  0) => Direction.E,
                ( 1,  1) => Direction.NE,
                ( 0,  1) => Direction.N,
                (-1,  1) => Direction.NW,
                (-1,  0) => Direction.W,
                (-1, -1) => Direction.SW,
                ( 0, -1) => Direction.S,
                ( 1, -1) => Direction.SE,
                _        => Direction.None
            };
        }
    }
}
