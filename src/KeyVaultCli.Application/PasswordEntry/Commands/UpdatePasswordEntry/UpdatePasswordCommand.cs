using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Application.PasswordEntry.Commands.UpdatePasswordEntry;

public class UpdatePasswordCommand(IVault vault, IConsole consoleService) : ICommand
{
    public void Execute()
    {
        var currentServiceName = consoleService.GetInputFromPrompt("Enter current service name: ");
        var currentAccountName = consoleService.GetInputFromPrompt("Enter current account name: ");
        var newServiceName = consoleService.GetInputFromPrompt("Enter new service name: ");
        var newAccountName = consoleService.GetInputFromPrompt("Enter new account name: ");
        var newPassword = consoleService.GetInputFromPrompt("Enter new password: ");

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