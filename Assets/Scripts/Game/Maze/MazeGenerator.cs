using System;
using System.Collections.Generic;
using System.Drawing;
using static IMazeGenerator;

public class MazeGenerator : IMazeGenerator
{
    // Move in that direction by n cells
    private static readonly Func<Point, int, Point>[] _directionFunctions =
    {
        // Up
        (Point coordinates, int steps) => { coordinates.X -= steps; return coordinates; },
        // Left
        (Point coordinates, int steps) => { coordinates.Y -= steps; return coordinates; },
        // Down
        (Point coordinates, int steps) => { coordinates.X += steps; return coordinates; },
        // Right
        (Point coordinates, int steps) => { coordinates.Y += steps; return coordinates; },
    };

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
                    maze[row, col] = CellType.Wall;
                }
                else
                {
                    maze[row, col] = CellType.None;
                }
            }
        }

        return maze;
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
        return !(row < 0 || col < 0 || row >= height || col >= width || maze[row, col] != CellType.None);
    }

    private readonly Random _random = new();

    /// <summary>
    /// The backtracking algorithm to carve a random path through the wall grid
    /// </summary>
    /// <param name="maze"></param>
    /// <param name="currentRow"></param>
    /// <param name="currentCol"></param>
    /// <param name="height"></param>
    /// <param name="width"></param>
    private void Generate(CellType[,] maze, int currentRow, int currentCol, int height, int width)
    {
        maze[currentRow, currentCol] = CellType.Path;

        // The number corresponds the the index of the movement in directionFunctions_
        List<int> directions = new List<int>(new int[] { 0, 1, 2, 3 }); // up, left, down, right

        for (int i = 0; i < 4; i++) // for all 4 directions
        {
            // Get a random neighbor
            Point neighbor = new Point(currentRow, currentCol);
            int chosenDirection = _random.Next(directions.Count);
            // Chosen direction is the index of the direction
            neighbor = _directionFunctions[directions[chosenDirection]].Invoke(neighbor, 2);

            if (IsAValidPath(maze, neighbor, height, width))
            {
                // We have to take down the wall between the current cell and the neighbor
                Point nearbyWall = new Point(currentRow, currentCol);
                nearbyWall = _directionFunctions[directions[chosenDirection]].Invoke(nearbyWall, 1);
                maze[nearbyWall.X, nearbyWall.Y] = CellType.Path;

                // Continue moving
                Generate(maze, neighbor.X, neighbor.Y, height, width);
            }

            // We will not go that way again
            directions.RemoveAt(chosenDirection);
        }
    }

    /// <summary>
    /// Randomly generates a maze of given dimensions.
    /// The start is always on the bottom line (max y) and the end on the upper line (y = 0)
    /// 
    /// A minimal maze is a simple corridor
    /// </summary>
    /// <param name="height">Height of the maze. Must be odd and superior or equal to 3</param>
    /// <param name="width">Width of the maze. Must be odd and superior or equal to 3</param>
    /// <returns>An array containing the type of each cell</returns>
    /// <exception cref="ArgumentException">if the parameters do not follow the rules</exception>
    public CellType[,] GenerateMaze(int height, int width)
    {
        if (height < 3 || height % 2 == 0 || width < 3 || width % 2 == 0)
        {
            throw new ArgumentException("Maze height and witdh must be superior or equal to 3 and odd");
        }

        CellType[,] maze = InitializeMaze(height, width);

        // Determine the column of the start of the maze
        int startCol = _random.Next(1, width - 1);
        // It falls on a wall
        if (startCol % 2 == 0)
        {
            startCol++;
        }

        Generate(maze, height - 2, startCol, height, width);

        int endCol = _random.Next(1, width - 1);
        if (maze[1, endCol] == CellType.Wall)
        {
            // The next one is part of the path
            endCol++;
        }

        // Connect the start to the exterior
        maze[height - 1, startCol] = CellType.Start;
        // Connect the end to the exterior
        maze[0, endCol] = CellType.Exit;

        return maze;
    }
}
