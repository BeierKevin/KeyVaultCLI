namespace KeyVaultCli.Presentation.UserInterface;

public class ColorGenerated
{
    // States Colors
    public CustomConsoleColor Success { get; } = new CustomConsoleColor(144, 238, 144);
    public CustomConsoleColor Info { get; } = new CustomConsoleColor(173, 216, 230);
    public CustomConsoleColor Warning { get; } = new CustomConsoleColor(255, 140, 0);
    public CustomConsoleColor Error { get; } = new CustomConsoleColor(255, 105, 97);
    // Default Colors
    public CustomConsoleColor White { get; } = new CustomConsoleColor(255, 255, 255);
    public CustomConsoleColor Black { get; } = new CustomConsoleColor(0, 0, 0);
    public CustomConsoleColor Red { get; } = new CustomConsoleColor(255, 0, 0);
    public CustomConsoleColor Green { get; } = new CustomConsoleColor(0, 128, 0);
    public CustomConsoleColor Blue { get; } = new CustomConsoleColor(0, 0, 255);
    public CustomConsoleColor Purple { get; } = new CustomConsoleColor(128, 0, 128); 
    public CustomConsoleColor Magenta { get; } = new CustomConsoleColor(255, 0, 255);
}