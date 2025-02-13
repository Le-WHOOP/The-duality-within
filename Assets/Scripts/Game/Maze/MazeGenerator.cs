using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public static class MazeGenerator
{
    private static readonly System.Random random_ = new();

    private static readonly Func<Point, int, Point>[] directionFunctions_ = // move in that direction by "step" cells
        {
                (Point coordinates, int steps) => { coordinates.X -= steps; return coordinates; }, // up
                (Point coordinates, int steps) => { coordinates.Y -= steps; return coordinates; }, // left
                (Point coordinates, int steps) => { coordinates.X += steps; return coordinates; }, // down
                (Point coordinates, int steps) => { coordinates.Y += steps; return coordinates; }  // right
        };


    /// <summary>
    /// Randomly generates a maze of given dimensions that has exactly one start and one end.
    /// The start is always on the bottom line (max y) and the end on the upper line (y = 0)
    /// 
    /// A minimal maze is a simple corridor
    /// </summary>
    /// <param name="height">Height of the maze. Must be odd and superior or equal to 3</param>
    /// <param name="width">Width of the maze. Must be odd and superior or equal to 3</param>
    /// <returns>An array containing the type of each cell</returns>
    /// <exception cref="ArgumentException">if the parameters do not follow the rules</exception>
    public static CellType[,] GenerateMaze(int height, int width)
    {
        if (height < 3 || height % 2 == 0 || width < 3 || width % 2 == 0)
        {
            throw new ArgumentException("Maze height and witdh must be superior or equal to 3 and odd");
        }

        CellType[,] maze = InitializeMaze(height, width);

        // determine the column of the start of the maze
        int startCol = random_.Next(1, width - 1);
        if (startCol % 2 == 0) // it falls on a wall
        {
            startCol++;
        }

        Generate(maze, height - 2, startCol, height, width);

        int endCol = random_.Next(1, width - 1);
        if (maze[1, endCol] == CellType.WALL)
        {
            endCol++; // the next one is part of the path
        }

        maze[height - 1, startCol] = CellType.PATH; // connect the start to the exterior
        maze[0, endCol] = CellType.PATH; // connect the end to the exterior

        return maze;
    }

    /// <summary>
    /// Creates the maze and fills it with the base values.
    /// In the begining, not yet visited path cells (of type CellType.NONE) are surrounded by walls (CellType.WALL)
    /// </summary>
    /// <param name="height">The height of the maze</param>
    /// <param name="width">The width of the maze</param>
    /// <returns>An array representing the maze</returns>
    private static CellType[,] InitializeMaze(int height, int width)
    {
        CellType[,] maze = new CellType[height, width];

        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                if (row % 2 == 0 || col % 2 == 0) // if one index is even, it's a wall
                {
                    // NOTE: external walls have an even index since the height and width are odd
                    maze[row, col] = CellType.WALL;
                }
                else
                {
                    maze[row, col] = CellType.NONE;
                }
            }
        }

        return maze;
    }

    /// <summary>
    /// The backtracking algorithm to carve a random path through the wall grid
    /// </summary>
    /// <param name="maze"></param>
    /// <param name="currentRow"></param>
    /// <param name="currentCol"></param>
    /// <param name="height"></param>
    /// <param name="width"></param>
    private static void Generate(CellType[,] maze, int currentRow, int currentCol, int height, int width)
    {
        maze[currentRow, currentCol] = CellType.PATH;

        // The number corresponds the the index of the movement in directionFunctions_
        List<int> directions = new List<int>(new int[] { 0, 1, 2, 3 }); // up, left, down, right

        for (int i = 0; i < 4; i++) // for all 4 directions
        {
            // get a random neighbor
            Point neighbor = new Point(currentRow, currentCol);
            int chosenDirection = random_.Next(directions.Count);
            // chosen direction is the index of the direction
            neighbor = directionFunctions_[directions[chosenDirection]].Invoke(neighbor, 2);

            if (IsAValidPath(maze, neighbor, height, width))
            {
                // we have to take down the wall between the current cell and the neighbor
                Point nearbyWall = new Point(currentRow, currentCol);
                nearbyWall = directionFunctions_[directions[chosenDirection]].Invoke(nearbyWall, 1);
                maze[nearbyWall.X, nearbyWall.Y] = CellType.PATH;

                // continue moving
                Generate(maze, neighbor.X, neighbor.Y, height, width);
            }

            directions.RemoveAt(chosenDirection); // we will not go that way again
        }
    }

    /// <summary>
    /// Checks if the neighbor is a valid cell to go to
    /// </summary>
    /// <param name="maze">The maze</param>
    /// <param name="neighbor">The coordinates of the cell we want to go to</param>
    /// <param name="height">Height of the maze</param>
    /// <param name="width">Witdh of the maze</param>
    /// <returns>true if the cell is in the maze and was not visited, false otherwise</returns>
    private static bool IsAValidPath(CellType[,] maze, Point neighbor, int height, int width)
    {
        int row = neighbor.X;
        int col = neighbor.Y;
        return !(row < 0 || col < 0 || row >= height || col >= width || maze[row, col] != CellType.NONE);
    }
}
