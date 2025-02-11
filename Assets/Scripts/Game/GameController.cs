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
        player1InputHandler.EntityController = cityPlayerController;
        player2InputHandler.EntityController = mazePlayerController;

        if (GameSettings.SwapRoles)
            SwapRoles();
    }

    private void SwapRoles()
    {
        (player2InputHandler.EntityController, player1InputHandler.EntityController) = (player1InputHandler.EntityController, player2InputHandler.EntityController);
    }
}
