using KeyVaultCli.Application;

namespace KeyVaultCli.Domain.Commands;

public class ExitCommand : ICommand
{
    private readonly IConsoleService _consoleService;

    public ExitCommand(IConsoleService consoleService)
    {
        _consoleService = consoleService;
    }
    public void Execute()
    {
        _consoleService.WriteError("Exiting application");
    }
}