using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyScreenController : ScreenController
{
    [Header("Player 1")]
    [SerializeField]
    private TextMeshProUGUI _player1DifficultyText;

    [SerializeField]
    private TextMeshProUGUI _player1NameText;

    [SerializeField]
    private Image _player1Image;

    [Header("Player 2")]
    [SerializeField]
    private TextMeshProUGUI _player2DifficultyText;

    [SerializeField]
    private TextMeshProUGUI _player2NameText;

    [SerializeField]
    private Image _player2Image;

    [Header("Scene")]
    [SerializeField]
    private FadeScene _fadeScene;

    /// <summary>
    /// Clamps the difficulty within the specified bounds.
    /// </summary>
    private Difficulty ClampDifficulty(Difficulty difficulty, Difficulty min, Difficulty max)
    {
        return difficulty < min ? min : (difficulty > max ? max : difficulty);
    }

    /// <summary>
    /// Adjusts the difficulty level within the allowed range.
    /// </summary>
    private Difficulty AdjustDifficulty(Difficulty currentDifficulty, int increment)
    {
        return ClampDifficulty(currentDifficulty + increment, Difficulty.Easy, Difficulty.Hard);
    }

    /// <summary>
    /// Changes Player 1's difficulty level by the given increment.
    /// </summary>
    public void ChangePlayer1Difficulty(int increment)
    {
        ChangePlayerDifficulty(1, increment);
    }

    /// <summary>
    /// Changes Player 2's difficulty level by the given increment.
    /// </summary>
    public void ChangePlayer2Difficulty(int increment)
    {
        ChangePlayerDifficulty(2, increment);
    }

    /// <summary>
    /// Changes the difficulty of the specified player.
    /// </summary>
    private void ChangePlayerDifficulty(int player, int increment)
    {
#if DEBUG
        if (player < 1 || player > 2)
            throw new ArgumentException("Invalid player number. Only Player 1 and Player 2 are available.");
#endif

        switch (player)
        {
            case 1:
                GameSettings.Player1Difficulty = AdjustDifficulty(GameSettings.Player1Difficulty, increment);
                _player1DifficultyText.text = GameSettings.Player1Difficulty.ToString();
                break;
            case 2:
                GameSettings.Player2Difficulty = AdjustDifficulty(GameSettings.Player2Difficulty, increment);
                _player2DifficultyText.text = GameSettings.Player2Difficulty.ToString();
                break;
        }
    }

    /// <summary>
    /// Swaps the roles of Player 1 and Player 2, including names and images.
    /// </summary>
    public void SwapRoles()
    {
        GameSettings.SwapRoles = !GameSettings.SwapRoles;

        // Swap player names and images
        string tempName = _player1NameText.text;
        Color tempColor = _player1Image.color;
        // TODO: When sprites are available, swap sprites instead of just colors
        // Sprite tempSprite = player1Image.sprite;

        _player1NameText.text = _player2NameText.text;
        _player2NameText.text = tempName;

        // player1Image.sprite = player2Image.sprite;
        // player2Image.sprite = tempSprite;
        _player1Image.color = _player2Image.color;
        _player2Image.color = tempColor;
    }

    /// <summary>
    /// Starts the game by loading the game scene.
    /// </summary>
    public void StartGame()
    {
        StartCoroutine(_fadeScene.LoadScene("GameScene"));
    }
}
