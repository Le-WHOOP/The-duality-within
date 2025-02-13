internal interface IMazeGenerator
{
    public enum CellType
    {
        WALL,
        PATH,
        // It means the cell has not yet been visited
        NONE,
    }

    CellType[,] GenerateMaze(int height, int width);
}
