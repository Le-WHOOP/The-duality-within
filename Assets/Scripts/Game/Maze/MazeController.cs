using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using static IMazeGenerator;

public class MazeController : MonoBehaviour
{
    private readonly IMazeGenerator _mazeGenerator = new MazeGenerator();

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
                    case CellType.NONE:
                        throw new ArgumentException("Who touched the maze generation ?");
                    case CellType.PATH:
                        _groundTilemap.SetTile(toTilemapPosition(x, y), _groundTile);
                        break;
                    case CellType.WALL:
                        _wallsTilemap.SetTile(toTilemapPosition(x, y), _wallsTile);
                        break;
                }
            }
        }

        _exitTilemap.SetTile(toTilemapPosition(0, 0), _exitTile);
    }

    public void GenerateMaze()
    {
        // TODO CHange hard coded size
        FillTilemaps(_mazeGenerator.GenerateMaze(11, 11));
        // TODO Move the player to the start
        _player.transform.localPosition = Vector3.zero;
        // TODO Set camera position
        //_camera.transform.localPosition = ;
    }
}
