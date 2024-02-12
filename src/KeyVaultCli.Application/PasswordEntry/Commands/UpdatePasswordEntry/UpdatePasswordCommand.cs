using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain;
using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Application.PasswordEntry.Commands.UpdatePasswordEntry;

public class UpdatePasswordCommand(IVault vault, IConsole consoleService) : ICommand
{
    public void Execute()
    {
        var currentServiceName = consoleService.GetInput("Enter current service name: ");
        var currentAccountName = consoleService.GetInput("Enter current account name: ");
        var newServiceName = consoleService.GetInput("Enter new service name: ");
        var newAccountName = consoleService.GetInput("Enter new account name: ");
        var newPassword = consoleService.GetInput("Enter new password: ");

        if (vault.UpdatePasswordEntry(currentServiceName, currentAccountName, newServiceName, newAccountName, newPassword.Length, newPassword))
        {
            consoleService.WriteSuccess("The password entry has been updated.");
        }
        else
        {
            consoleService.WriteError("Failed to update the password entry. Ensure the service and account exists.");
        }
    }
}