using KeyVaultCli.Domain;

namespace KeyVaultCli.Application.PasswordEntry.Commands.CreatePasswordEntry;

public class CreatePasswordGenerateCommand(IVault vault, IConsole consoleService) : ICommand
{
    public void Execute()
    {
        var serviceName = consoleService.GetInput("Enter service name for the new password: ");
        var accountName = consoleService.GetInput("Enter account name for the new password: ");
        var passwordLengthStr = consoleService.GetInput("Enter the desired password length: ");

        if (!int.TryParse(passwordLengthStr, out var passwordLength))
        {
            consoleService.WriteError("Invalid input for password length. Ensure you enter a valid number.");
            return;
        }

        var password = vault.GenerateAndAddPasswordEntry(serviceName, accountName, passwordLength);

        consoleService.WriteSuccess($"A new password has been created and stored for {serviceName}, {accountName} with the value {password}.");
    }
}