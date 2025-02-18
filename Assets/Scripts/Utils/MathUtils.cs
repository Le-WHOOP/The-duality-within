using System;

internal static class MathUtils
{
    public static int OddFloor(double value)
    {
        var floor = (int)Math.Floor(value);
        return floor - (1 - (floor % 2));
    }
}
