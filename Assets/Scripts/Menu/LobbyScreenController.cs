using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LobbyScreenController : ScreenController
{
    [Header("Player 1")]
    [SerializeField]
    private TextMeshProUGUI p1DifficultyText;

    [SerializeField]
    private TextMeshProUGUI p1NameText;

    [SerializeField]
    private Image p1Image;

    [Header("Player 2")]
    [SerializeField]
    private TextMeshProUGUI p2DifficultyText;

    [SerializeField]
    private TextMeshProUGUI p2NameText;

    [SerializeField]
    private Image p2Image;

    [Header("Scene")]
    [SerializeField]
    private FadeScene fadeScene;

    private Difficulty BoundDifficulty(Difficulty difficulty, Difficulty min, Difficulty max)
    {
        return difficulty < min ? min : (difficulty > max ? max : difficulty);
    }

    private Difficulty Truc(Difficulty currentDifficulty, int increment)
    {
        return BoundDifficulty(currentDifficulty + increment, Difficulty.Easy, Difficulty.Hard);
    }

    public void ChangePlayer1Difficulty(int increment)
    {
        ChangePlayerDifficulty(1, increment);
    }

    public void ChangePlayer2Difficulty(int increment)
    {
        ChangePlayerDifficulty(2, increment);
    }

    // Unity buttons can't call methods that have more that one argument in the editor
    private void ChangePlayerDifficulty(int player, int increment)
    {
#if DEBUG
        if (player < 1 || player > 2)
            throw new ArgumentException("There is only two players to choose from, you donkey");
#endif

        switch (player)
        {
            case 1:
                GameSettings.Player1Difficulty = Truc(GameSettings.Player1Difficulty, increment);
                p1DifficultyText.text = GameSettings.Player1Difficulty.ToString();
                break;
            case 2:
                GameSettings.Player2Difficulty = Truc(GameSettings.Player2Difficulty, increment);
                p2DifficultyText.text = GameSettings.Player2Difficulty.ToString();
                break;
        }
    }

    public void SwapRoles()
    {
        GameSettings.SwapRoles = !GameSettings.SwapRoles;

        // Swaping Jekyll & Hyde and the image
        string tmpText = p1NameText.text;
        Color tmpImage = p1Image.color;
        // TODO: When the sprites are on the projet, uncomment the lines of codes and delete en .color ones
        //Sprite tmpSprite = p1Image.sprite;

        p1NameText.text = p2NameText.text;
        p2NameText.text = tmpText;

        //p1Image.sprite = p2Image.sprite;
        //p2Image.sprite = tmpSprite;
        p1Image.color = p2Image.color;
        p2Image.color = tmpImage;
    }

    public void StartGame()
    {
        StartCoroutine(fadeScene.LoadScene("GameScene"));
    }
}
