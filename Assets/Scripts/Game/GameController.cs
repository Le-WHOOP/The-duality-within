using UnityEngine;

public class GameController : MonoBehaviour
{
    private const float city_screen_ratio = 0.7f;
    // TODO Find a better name
    private const float maze_screen_ratio = 1 - city_screen_ratio;

    [SerializeField]
    private InputHandler player1InputHandler;
    [SerializeField]
    private InputHandler player2InputHandler;

    [SerializeField]
    private PlayerController cityPlayerController;
    [SerializeField]
    private PlayerController mazePlayerController;

    [SerializeField]
    private Camera cityCamera;
    [SerializeField]
    private Camera mazeCamera;

    void Start()
    {
        player1InputHandler.EntityController = cityPlayerController;
        player2InputHandler.EntityController = mazePlayerController;

        if (GameSettings.SwapRoles)
            SwapRoles();
    }

    public void SwapRoles()
    {
        (player2InputHandler.EntityController, player1InputHandler.EntityController) = (player1InputHandler.EntityController, player2InputHandler.EntityController);

        cityCamera.rect = new Rect(cityCamera.rect)
        {
            x = maze_screen_ratio - cityCamera.rect.x,
            width = (1 + city_screen_ratio) - cityCamera.rect.width,
        };
        mazeCamera.rect = new Rect(mazeCamera.rect)
        {
            x = city_screen_ratio - mazeCamera.rect.x,
            width = (1 + maze_screen_ratio) - mazeCamera.rect.width,
        };
    }
}
