/// <summary>
/// Stores global game settings that affect gameplay behavior.
/// </summary>
/// <remarks>
/// Stores everything statically since instances are all destroyed when a new scene is loaded
/// </remarks>
public static class GameSettings
{
    /// <summary>
    /// Determines what player plays what character
    /// </summary>
    /// <remarks>
    /// When <see langword="false"/>, player 1 will play jekyll and player 2 will play hyde.
    /// When <see langword="true"/>, it will be the opposite
    /// </remarks>
    public static bool SwapRoles { get; set; }

    /// <summary>
    /// The difficulty level assigned to Player 1. Defaults to Medium.
    /// </summary>
    public static Difficulty Player1Difficulty { get; set; } = Difficulty.Medium;

    /// <summary>
    /// The difficulty level assigned to Player 2. Defaults to Medium.
    /// </summary>
    public static Difficulty Player2Difficulty { get; set; } = Difficulty.Medium;
}
