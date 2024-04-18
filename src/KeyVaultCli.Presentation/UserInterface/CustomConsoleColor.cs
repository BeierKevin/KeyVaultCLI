using System;

namespace KeyVaultCli.Presentation.UserInterface;

public struct CustomConsoleColor
{
    public byte R { get; }
    public byte G { get; }
    public byte B { get; }

    public CustomConsoleColor(byte r, byte g, byte b)
    {
        R = r;
        G = g;
        B = b;
    }

    public override bool Equals(object obj)
    {
        if (!(obj is CustomConsoleColor))
            return false;
        
        var color = (CustomConsoleColor) obj;
        return R == color.R && G == color.G && B == color.B;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(R, G, B);
    }

    public override string ToString()
    {
        return $"R: {R}, G: {G}, B: {B}";
    }

    // Equality operator.
    public static bool operator ==(CustomConsoleColor a, CustomConsoleColor b) => a.Equals(b);

    // Inequality operator.
    public static bool operator !=(CustomConsoleColor a, CustomConsoleColor b) => !a.Equals(b);

    public string ToAnsiEscapeSequence()
    {
        return $"\u001b[38;2;{R};{G};{B}m";
    }
}	