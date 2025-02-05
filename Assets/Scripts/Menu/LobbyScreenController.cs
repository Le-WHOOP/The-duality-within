using System;
using UnityEngine.SceneManagement;

public class LobbyScreenController : ScreenController
{
    private Difficulty BoundDifficulty(Difficulty difficulty, Difficulty min, Difficulty max)
    {
        return difficulty < min ? min : difficulty > max ? max : difficulty;
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
                break;
            case 2:
                GameSettings.Player2Difficulty = Truc(GameSettings.Player2Difficulty, increment);
                break;
        }
    }

    public void SwapRoles()
    {
        GameSettings.SwapRoles = !GameSettings.SwapRoles;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
