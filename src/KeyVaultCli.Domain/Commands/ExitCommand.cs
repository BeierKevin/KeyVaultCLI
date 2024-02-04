namespace KeyVaultCli.Domain.Commands;

public class ExitCommand : ICommand
{
    private readonly IConsole _consoleService;

    public ExitCommand(IConsole consoleService)
    {
        _consoleService = consoleService;
    }
    public void Execute()
    {
        _consoleService.WriteError("Exiting application");
    }
}