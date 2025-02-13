using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private InputHandler player1InputHandler;

    [SerializeField]
    private InputHandler player2InputHandler;

    [SerializeField]
    private PlayerController cityPlayerController;

    [SerializeField]
    private PlayerController mazePlayerController;

    void Start()
    {
        player1InputHandler.Player = cityPlayerController;
        player2InputHandler.Player = mazePlayerController;

        if (GameSettings.SwapRoles)
            SwapRoles();
    }

    private void SwapRoles()
    {
        (player2InputHandler.Player, player1InputHandler.Player) = (player1InputHandler.Player, player2InputHandler.Player);
    }
}
