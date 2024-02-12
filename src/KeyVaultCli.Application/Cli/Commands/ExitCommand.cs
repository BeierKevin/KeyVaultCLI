using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain;
using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Application.Cli.Commands;

public class ExitCommand(IConsole consoleService) : ICommand
{
    public void Execute()
    {
        consoleService.WriteError("Exiting application");
    }
}