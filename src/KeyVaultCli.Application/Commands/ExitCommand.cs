using KeyVaultCli.Domain;

namespace KeyVaultCli.Application.Commands;

public class ExitCommand(IConsole consoleService) : ICommand
{
    public void Execute()
    {
        consoleService.WriteError("Exiting application");
    }
}