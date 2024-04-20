namespace KeyVaultCli.Presentation.UserInterface;

public class Text
{
    private string text;
    private ConsoleColor color;
    private readonly string colorSequence;
    private readonly bool isBold;

    public Text(string text, CustomConsoleColor color, bool isBold = false)
    {
        this.text = text;
        this.isBold = isBold;
        colorSequence = color.ToAnsiEscapeSequence();
    }

    public void DisplayText()
    {
        var boldSequence = isBold ? "\x1b[1m" : "";
        Console.Write(colorSequence);
        Console.Write(boldSequence);
        Console.Write(text);
        Console.WriteLine("\u001b[0m");
    }
}