using UnityEngine;

public class MazePlayerController : PlayerController
{
    public MazePlayerController() : base(Personnality.Hyde) { }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // TODO Trigger when the player reached the maze exit
    }
}
