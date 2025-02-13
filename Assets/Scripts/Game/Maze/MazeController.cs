using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MazeController : MonoBehaviour
{
    private readonly IMazeGenerator _mazeGenerator;

    [Header("Tilemaps")]
    [SerializeField]
    private Tilemap _groundTilemap;
    [SerializeField]
    private Tilemap _wallsTilemap;
    [SerializeField]
    private Tilemap _exitTilemap;

    [Header("Player")]
    [SerializeField]
    // TODO Change type to maze player when merge is done
    private PlayerController _player;

    private void FillTilemaps(object maze)
    {
        // TODO Fill tilemaps according to the new maze
        throw new NotImplementedException();
    }

    public void GenerateMaze()
    {
        // TODO CHange hard coded size
        FillTilemaps(_mazeGenerator.GenerateMaze(10, 10));
        // TODO Move the player to the start
        _player.transform.position = Vector3.zero;
    }
}
