using System;

public static class Utils
{
    // TODO Unused
    public static T Bound<T>(T value, T min, T max) where T : IComparable<T>
    {
        return value.CompareTo(min) < 0 ? min : value.CompareTo(max) > 0 ? max : value;
    }
}
