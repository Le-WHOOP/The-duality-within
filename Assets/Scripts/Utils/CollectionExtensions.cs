using System;

internal static class CollectionExtensions
{
    public static (int X, int Y) IndexOf<T>(this T[,] values, Func<T, bool> predicate)
    {
        for (int y = 0; y < values.GetLength(0); y++)
            for (int x = 0; x < values.GetLength(1); x++)
                if (predicate(values[y, x]))
                    return (x, y);
        return (-1, -1);
    }
}
