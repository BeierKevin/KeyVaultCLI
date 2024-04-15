using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Application.PasswordEntry.Commands.CreatePasswordEntry;

public class CreatePasswordGenerateCommand(IVault vault, IConsole consoleService) : ICommand
{
    public void Execute()
    {
        var serviceName = consoleService.GetInputFromPrompt("Enter service name for the new password: ");
        var accountName = consoleService.GetInputFromPrompt("Enter account name for the new password: ");
        var passwordLengthStr = consoleService.GetInputFromPrompt("Enter the desired password length: ");
        var url = consoleService.GetInputFromPrompt("Enter the URL (leave empty if not applicable): "); // Use the service
        var category = consoleService.GetInputFromPrompt("Enter the category (leave empty if not applicable): "); // Use the service

        if (!int.TryParse(passwordLengthStr, out var passwordLength))
        {
            consoleService.WriteError("Invalid input for password length. Ensure you enter a valid number.");
            return;
        }

        // Generate password internally and add a password entry
        var password = vault.GenerateAndAddPasswordEntry(serviceName, accountName, passwordLength, url, category);

        consoleService.WriteSuccess($"A new password has been created and stored for {serviceName}, {accountName} with the value {password}.");
    }
}