using KeyVaultCli.Application;
using KeyVaultCli.Core;
using KeyVaultCli.Infrastructure.UI;

namespace KeyVaultCli.Commands;

public class UpdatePasswordGeneratedCommand : ICommand
{
    private readonly Vault _vault;
    private readonly IConsoleService _consoleService;

    public UpdatePasswordGeneratedCommand(Vault vault, IConsoleService consoleService)
    {
        _vault = vault;
        _consoleService = new ConsoleService();
    }

    public void Execute()
    {
        var oldServiceName = _consoleService.GetInput("Enter old service name: ");
        var oldAccountName = _consoleService.GetInput("Enter old account name: ");
        var newServiceName = _consoleService.GetInput("Enter new service name: ");
        var newAccountName = _consoleService.GetInput("Enter new account name: ");
        var passwordLengthInput = _consoleService.GetInput("Enter the number of characters for the new password (e.g. 10): ");

        if (!int.TryParse(passwordLengthInput, out var passwordLength))
        {
            _consoleService.WriteError("Invalid input for password length. Ensure you enter a valid number.");
            return;
        }

        if (_vault.UpdatePasswordEntry(oldServiceName, oldAccountName, newServiceName, newAccountName, passwordLength))
        {
            _consoleService.WriteSuccess($"Password entry for {oldServiceName}, {oldAccountName} has been updated with new details.");
        }
        else
        {
            _consoleService.WriteError("Failed to update the password entry. Ensure the old service and account names are correct.");
        }
    }
}