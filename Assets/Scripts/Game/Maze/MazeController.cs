using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using static IMazeGenerator;

public class MazeController : MonoBehaviour
{
    private readonly IMazeGenerator _mazeGenerator = new MazeGenerator();

    [Header("Game Controller")]
    [SerializeField]
    private GameController _gameController;

    [Header("Tilemaps")]
    [SerializeField]
    private Tilemap _groundTilemap;
    [SerializeField]
    private Tilemap _wallsTilemap;
    [SerializeField]
    private Tilemap _exitTilemap;

    [Header("Tiles")]
    [SerializeField]
    private TileBase _groundTile;
    [SerializeField]
    private TileBase _wallsTile;
    [SerializeField]
    private TileBase _startTile;
    [SerializeField]
    private TileBase _exitTile;

    [Header("Player")]
    [SerializeField]
    // TODO Change type to maze player when merge is done
    private PlayerController _player;

    [Header("Camera")]
    [SerializeField]
    private Camera _camera;

    void Start()
    {
        _exitTilemap.GetComponent<TriggerController>().OnTriggerEntered += (_, collider) =>
        {
            // TODO Is it necessaery to check if the collision is with the player or not ?
            _gameController.SwapRoles();
            GenerateMaze();
        };

        GenerateMaze();
    }

    /// <summary>
    /// Mapp the array index to the corrseponding tilemap index
    /// </summary>
    /// <param name="maze">The maze layout</param>
    /// <param name="x">The index of the column in the array</param>
    /// <param name="y">The index of the row in the array</param>
    /// <returns>A <see cref="Vector3Int"/></returns>
    private Vector3Int ToTilemapPosition(CellType[,] maze, int x, int y)
    {
        return new Vector3Int(x, maze.GetLength(0) - 1 - y);
    }

    /// <summary>
    /// Build all tilemaps according to the given maze layout
    /// </summary>
    /// <param name="maze">The maze layout</param>
    /// <exception cref="ArgumentException">Thrown when the maze contains invalid cells</exception>
    private void FillTilemaps(CellType[,] maze)
    {
        int rowCount = maze.GetLength(0);
        int columnCount = maze.GetLength(1);
        
        _groundTilemap.ClearAllTiles();
        _wallsTilemap.ClearAllTiles();
        _exitTilemap.ClearAllTiles();

        for (int y = 0; y < rowCount; y++)
        {
            for (int x = 0; x < columnCount; x++)
            {
                Vector3Int position = ToTilemapPosition(maze, x, y);

                // I hate 2d arrays
                switch (maze[y, x])
                {
                    case CellType.None:
                        throw new ArgumentException("Who touched the maze generation ?");
                    case CellType.Path:
                        _groundTilemap.SetTile(position, _groundTile);
                        break;
                    case CellType.Wall:
                        _wallsTilemap.SetTile(position, _wallsTile);
                        break;
                    case CellType.Start:
                        _groundTilemap.SetTile(position, _startTile);
                        break;
                    case CellType.Exit:
                        _exitTilemap.SetTile(position, _exitTile);
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Moves the player at the start of the maze
    /// </summary>
    /// <param name="maze">the maze layout</param>
    private void ResetPlayerPosition(CellType[,] maze)
    {
        // First, get the start index
        (int X, int Y) startPosition = maze.IndexOf(cell => cell == CellType.Start);
        // Then transform this array index to a tilemap index
        _player.transform.localPosition = (Vector3)ToTilemapPosition(maze, startPosition.X, startPosition.Y)
            // Multiplies this index by the size of the cells to get a transform position
            // Then add half of a cell size to the position to place the player in the middle of the cell center
            * _groundTilemap.cellSize.x + new Vector3(_groundTilemap.cellSize.x / 2, _groundTilemap.cellSize.y / 2);
    }

    /// <summary>
    /// Moves the camera so that the entire maze is visible
    /// </summary>
    /// <param name="rowCount">The number of rows in the maze</param>
    /// <param name="columnCount">The number of columns in the maze</param>
    private void ResetCameraPosition(int rowCount, int columnCount)
    {
        // TODO Still need some work, but it'll do for now

        float cellSize = _groundTilemap.cellSize.x;
        float mazeHeight = cellSize * rowCount;
        float mazeWidth = cellSize * columnCount;

        _camera.orthographicSize = mazeHeight / 2f;

        // Center the camera on the maze
        _camera.transform.localPosition = new Vector3(mazeWidth / 2f, mazeHeight / 2f, -10);
    }

    /// <summary>
    /// Generate a new maze and move all related objects to their correct position
    /// </summary>
    public void GenerateMaze()
    {
        // TODO Change hard coded size
        CellType[,] maze = _mazeGenerator.GenerateMaze(33, 21);
        FillTilemaps(maze);

        // Move the player to the start
        ResetPlayerPosition(maze);

        // Set camera position so that the entire maze is easily visible
        ResetCameraPosition(maze.GetLength(0), maze.GetLength(1));
    }
}
