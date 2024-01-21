using KeyVaultCli.Application;
using KeyVaultCli.Domain.Entities;

namespace KeyVaultCli.Domain.Commands;

public class DeletePasswordCommand : ICommand
{
    private readonly Vault _vault;
    private readonly IConsoleService _consoleService;

    public DeletePasswordCommand(Vault vault, IConsoleService consoleService)
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