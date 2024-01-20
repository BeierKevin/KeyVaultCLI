namespace KeyVaultCli.Commands;

public class ExitCommand : ICommand
{
    public void Execute()
    {
        Console.WriteLine("Exiting application");
    }
}