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
        player1InputHandler.PlayerController = cityPlayerController;
        player2InputHandler.PlayerController = mazePlayerController;

        if (GameSettings.SwapRoles)
            SwapRoles();
    }

    public void SwapRoles()
    {
        (player2InputHandler.PlayerController, player1InputHandler.PlayerController) = (player1InputHandler.PlayerController, player2InputHandler.PlayerController);
    }
}
