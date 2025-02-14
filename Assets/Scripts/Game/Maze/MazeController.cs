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

    private void FillTilemaps(CellType[,] maze)
    {
        int rowCount = maze.GetLength(0);
        int columnCount = maze.GetLength(1);
        
        Vector3Int toTilemapPosition(int x, int y) => new Vector3Int(rowCount - x, y);

        _groundTilemap.ClearAllTiles();
        _wallsTilemap.ClearAllTiles();
        _exitTilemap.ClearAllTiles();

        for (int x = 0; x < rowCount; x++)
        {
            for (int y = 0; y < columnCount; y++)
            {
                // I hate 2d arrays
                switch (maze[y, x])
                {
                    case CellType.None:
                        throw new ArgumentException("Who touched the maze generation ?");
                    case CellType.Path:
                        _groundTilemap.SetTile(toTilemapPosition(x, y), _groundTile);
                        break;
                    case CellType.Wall:
                        _wallsTilemap.SetTile(toTilemapPosition(x, y), _wallsTile);
                        break;
                    case CellType.Start:
                        _groundTilemap.SetTile(toTilemapPosition(x, y), _startTile);
                        break;
                    case CellType.Exit:
                        _exitTilemap.SetTile(toTilemapPosition(x, y), _exitTile);
                        break;
                }
            }
        }
    }

    public void GenerateMaze()
    {
        // TODO CHange hard coded size
        CellType[,] maze = _mazeGenerator.GenerateMaze(11, 11);
        FillTilemaps(maze);
        // TODO Move the player to the start
        //_player.transform.localPosition = ;
        // TODO Set camera position
        //_camera.transform.localPosition = ;
    }
}
