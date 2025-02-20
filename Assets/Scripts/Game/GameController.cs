using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

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

    [Header("Transition")]
    [SerializeField]
    private FadeScene _fadeScene;

    public GameController()
    {
        if (Instance != null)
            throw new Exception("There is an impostor among us");
        Instance = this;
    }

    void Start()
    {
        player1InputHandler.Player = cityPlayerController;
        player2InputHandler.Player = mazePlayerController;

        if (GameSettings.SwapRoles)
            SwapPersonnalities();
    }

    /// <summary>
    /// Come on, you really need a comment to understand this method ?
    /// </summary>
    private void SwapPersonnalities()
    {
        cityPlayerController.SwapPersonnality();
        mazePlayerController.SwapPersonnality();
    }

    public void SwapRoles()
    {
        // Invert the controls, so that each player now control the other character
        (player2InputHandler.Player, player1InputHandler.Player) = (player1InputHandler.Player, player2InputHandler.Player);

        // Swap the personnality, to update the sprites and the available interactions
        SwapPersonnalities();

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

    public void EndGame()
    {
        // You can only win when you are the one in control
        CityPlayerController winner = cityPlayerController;
        GameResults.Winner = player1InputHandler.Player == winner ? 1 : 2;
        StartCoroutine(_fadeScene.LoadScene("EndScene"));
    }
}
