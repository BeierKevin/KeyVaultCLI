using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain;
using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Application.PasswordEntry.Commands.DeletePasswordEntry;

public class DeletePasswordCommand(IVault vault, IConsole consoleService) : ICommand
{
    public void Execute()
    {
        var serviceName = consoleService.GetInputFromPrompt("Enter the service name for the password you want to delete: ");
        var accountName = consoleService.GetInputFromPrompt("Enter the account name for the password you want to delete: ");

        var isDeleted = vault.DeletePasswordEntry(serviceName, accountName);

        if (isDeleted)
        {
            consoleService.WriteSuccess("Password entry has been deleted.");
        }
        else
        {
            consoleService.WriteError("Failed to delete the password entry. Ensure the service and account names are correct.");
        }
    }
}