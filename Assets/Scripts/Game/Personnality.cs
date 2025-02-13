using System;

[Flags]
public enum Personnality
{
    // Unused value, but just in case it is ever needed
    None = 0,

    Jekyll = 1 << 0,
    Hyde = 1 << 1,

    // This value is used for interactions that can be used by both players
    Both = Jekyll | Hyde,
}
