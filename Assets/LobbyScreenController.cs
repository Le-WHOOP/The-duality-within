using System;

public class LobbyScreenController : ScreenController
{
    // TODO assign this
    private IGameController gameController;

    private bool swapRoles;

    private Difficulty player1Difficulty;
    private Difficulty player2Difficulty;

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
                player1Difficulty = Truc(player1Difficulty, increment);
                break;
            case 2:
                player2Difficulty = Truc(player2Difficulty, increment);
                break;
        }
    }

    public void SwapRoles()
    {
        swapRoles = !swapRoles;
    }

    public void StartGame()
    {
        gameController.StartGame(swapRoles, player1Difficulty, player2Difficulty);
    }
}
