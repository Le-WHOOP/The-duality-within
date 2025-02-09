using System.Drawing;

public static class GameSettings
{
    public static bool SwapRoles { get; set; }

    public static Difficulty Player1Difficulty { get; set; } = Difficulty.Medium;

    public static Difficulty Player2Difficulty { get; set; } = Difficulty.Medium;

    public static Point[] ParkSpawnPoints { get; } = { }; // TODO: define spawn coordinates
}
