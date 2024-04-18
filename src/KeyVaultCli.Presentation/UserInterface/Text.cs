namespace KeyVaultCli.Presentation.UserInterface;

public class Text
{
    private string text;
    private ConsoleColor color;
    private readonly string colorSequence;

    public Text(string text, CustomConsoleColor color)
    {
        this.text = text;
        colorSequence = color.ToAnsiEscapeSequence();
    }

    public void DisplayText()
    {
        Console.Write(colorSequence);
        Console.Write(text);
        Console.WriteLine("\u001b[0m");
    }
}