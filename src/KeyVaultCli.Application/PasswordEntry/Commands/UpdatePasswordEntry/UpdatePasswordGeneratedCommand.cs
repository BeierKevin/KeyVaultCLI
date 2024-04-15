using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Application.PasswordEntry.Commands.UpdatePasswordEntry;

public class UpdatePasswordGeneratedCommand(IVault vault, IConsole consoleService) : ICommand
{
    public void Execute()
    {
        var oldServiceName = consoleService.GetInputFromPrompt("Enter old service name: ");
        var oldAccountName = consoleService.GetInputFromPrompt("Enter old account name: ");
        var newServiceName = consoleService.GetInputFromPrompt("Enter new service name: ");
        var newAccountName = consoleService.GetInputFromPrompt("Enter new account name: ");
        var newUrl = consoleService.GetInputFromPrompt("Enter new URL: ");
        var newCategory = consoleService.GetInputFromPrompt("Enter new Category: ");
        var passwordLengthInput = consoleService.GetInputFromPrompt("Enter the number of characters for the new password (e.g. 10): ");

        if (!int.TryParse(passwordLengthInput, out var passwordLength))
        {
            consoleService.WriteError("Invalid input for password length. Ensure you enter a valid number.");
            return;
        }

        if (vault.UpdatePasswordEntry(oldServiceName, oldAccountName, newServiceName, newAccountName, passwordLength,
            null, newUrl: newUrl, newCategory: newCategory))
        {
            consoleService.WriteSuccess($"Password entry for {oldServiceName}, {oldAccountName} has been updated with new details.");
        }
        else
        {
            consoleService.WriteError("Failed to update the password entry. Ensure the old service and account names are correct.");
        }
    }
}