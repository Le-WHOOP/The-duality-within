using UnityEngine;

public class GameController : MonoBehaviour
{
    private const float city_screen_ratio = 0.6f;
    // TODO Find a better name
    private const float maze_screen_ratio = 1 - city_screen_ratio;

    [Header("Player input handlers")]
    [SerializeField]
    private InputHandler player1InputHandler;
    [SerializeField]
    private InputHandler player2InputHandler;

    [Header("Player controllers")]
    [SerializeField]
    private CityPlayerController cityPlayerController;
    [SerializeField]
    private MazePlayerController mazePlayerController;

    [Header("Cameras")]
    [SerializeField]
    private Camera cityCamera;
    [SerializeField]
    private Camera mazeCamera;

    void Start()
    {
        player1InputHandler.Player = cityPlayerController;
        player2InputHandler.Player = mazePlayerController;

        if (GameSettings.SwapRoles)
            SwapRoles();
    }

    public void SwapRoles()
    {
        // Invert the controls, so that each player now control the other character
        (player2InputHandler.Player, player1InputHandler.Player) = (player1InputHandler.Player, player2InputHandler.Player);

        // Swap the personnality, to update the sprites and the available interactions
        cityPlayerController.SwapPersonnality();
        mazePlayerController.SwapPersonnality();

        // Invert the camera layout, to align each player with the charcater it controls
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
