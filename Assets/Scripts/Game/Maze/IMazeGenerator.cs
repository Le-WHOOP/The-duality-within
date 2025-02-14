public interface IMazeGenerator
{
    public enum CellType
    {
        // None means that the cell has not yet been visited
        None,
        Start,
        Exit,
        Wall,
        Path,
    }

    CellType[,] GenerateMaze(int height, int width);
}
