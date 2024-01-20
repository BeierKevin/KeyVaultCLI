using KeyVaultCli.UI;

namespace KeyVaultCli.Commands;

public class ExitCommand : ICommand
{
    public void Execute()
    {
        ConsoleHelper.WriteError("Exiting application");
    }
}