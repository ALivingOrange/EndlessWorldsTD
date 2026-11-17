/***************************************************************
*file: Direction.cs
*author: Ethan Buff
*class: CS 4700 – Game Development
*assignment: program 4
*date last modified: 11/16/2026
*
*purpose: It's just an enum that's used for AI calculations
*         e.g. in FlowField.cs
*
****************************************************************/
using System;
using System.Collections.Immutable;

namespace AI
{
    // Starts E and goes counter clockwise Unit Circle math style
    // Everything that iterates through directions should prefer this order
    public enum Direction
    { None, E, NE, N, NW, W, SW, S, SE}

    public static class DirectionHelp 
    {
        // I feel like having these available here makes it more readable where they're used
        private static readonly Direction[] dir_cardinal = { Direction.E, Direction.N, Direction.W, Direction.S};
        private static readonly Direction[] dir_ordinal = { Direction.NE, Direction.NW, Direction.SW, Direction.SE};
        public static ReadOnlySpan<Direction> CARDINAL_DIRS => dir_cardinal;
        public static ReadOnlySpan<Direction> ORDINAL_DIRS => dir_ordinal;


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

        // function: DirToInts
        // purpose: Converts the direction enum to two integers indicating direction
        public static (int x, int y) DirToInts(Direction input)
        {
            return input switch
            {
                Direction.None => ( 0,  0),
                Direction.E    => ( 1,  0),
                Direction.NE   => ( 1,  1),
                Direction.N    => ( 0,  1),
                Direction.NW   => (-1,  1),
                Direction.W    => (-1,  0),
                Direction.SW   => (-1, -1),
                Direction.S    => ( 0, -1),
                Direction.SE   => ( 1, -1),
                _              => ( 0,  0)
            };
        }
    }
}
