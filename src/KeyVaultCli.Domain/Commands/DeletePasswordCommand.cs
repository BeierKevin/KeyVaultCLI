namespace KeyVaultCli.Domain.Commands;

public class DeletePasswordCommand : ICommand
{
    private readonly IVault _vault;
    private readonly IConsole _consoleService;

    public DeletePasswordCommand(IVault vault, IConsole consoleService)
    {
        _vault = vault;
        _consoleService = consoleService;
    }

    public void Execute()
    {
        var serviceName = _consoleService.GetInput("Enter the service name for the password you want to delete: ");
        var accountName = _consoleService.GetInput("Enter the account name for the password you want to delete: ");

        var isDeleted = _vault.DeletePasswordEntry(serviceName, accountName);

        if (isDeleted)
        {
            _consoleService.WriteSuccess("Password entry has been deleted.");
        }
        else
        {
            _consoleService.WriteError("Failed to delete the password entry. Ensure the service and account names are correct.");
        }
    }
}