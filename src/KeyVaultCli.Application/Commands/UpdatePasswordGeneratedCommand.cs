using KeyVaultCli.Domain;

namespace KeyVaultCli.Application.Commands;

public class UpdatePasswordGeneratedCommand(IVault vault, IConsole consoleService) : ICommand
{
    public void Execute()
    {
        var oldServiceName = consoleService.GetInput("Enter old service name: ");
        var oldAccountName = consoleService.GetInput("Enter old account name: ");
        var newServiceName = consoleService.GetInput("Enter new service name: ");
        var newAccountName = consoleService.GetInput("Enter new account name: ");
        var passwordLengthInput = consoleService.GetInput("Enter the number of characters for the new password (e.g. 10): ");

        if (!int.TryParse(passwordLengthInput, out var passwordLength))
        {
            consoleService.WriteError("Invalid input for password length. Ensure you enter a valid number.");
            return;
        }

        if (vault.UpdatePasswordEntry(oldServiceName, oldAccountName, newServiceName, newAccountName, passwordLength))
        {
            consoleService.WriteSuccess($"Password entry for {oldServiceName}, {oldAccountName} has been updated with new details.");
        }
        else
        {
            consoleService.WriteError("Failed to update the password entry. Ensure the old service and account names are correct.");
        }
    }
}