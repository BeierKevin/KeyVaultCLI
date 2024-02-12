using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain;

namespace KeyVaultCli.Application.Cli.Commands;

public class ExitCommand(IConsole consoleService) : ICommand
{
    public void Execute()
    {
        consoleService.WriteError("Exiting application");
    }
}