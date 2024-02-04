namespace KeyVaultCli.Domain.Commands;

public class DeleteAllPasswordsCommand : ICommand
{
    private readonly IVault _vault;
    private readonly IConsole _consoleService;

    public DeleteAllPasswordsCommand(IVault vault, IConsole consoleService)
    {
        _vault = vault;
        _consoleService = consoleService;
    }

    public void Execute()
    {
        var confirmation = _consoleService.GetInput("Are you sure you want to delete all passwords? (yes/no): ");
        if (confirmation.ToLower() == "yes")
        {
            _vault.DeleteAllPasswordEntries();
            _consoleService.WriteSuccess("All passwords have been deleted.");
        }
        else
        {
            _consoleService.WriteError("Operation cancelled.");
        }
    }
}