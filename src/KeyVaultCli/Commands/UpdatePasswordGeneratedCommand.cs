using CLI.UI;

namespace CLI.Commands;

public class UpdatePasswordGeneratedCommand : ICommand
{
    private readonly Vault _vault;

    public UpdatePasswordGeneratedCommand(Vault vault)
    {
        _vault = vault;
    }

    public void Execute()
    {
        var oldServiceName = ConsoleHelper.GetInput("Enter old service name: ");
        var oldAccountName = ConsoleHelper.GetInput("Enter old account name: ");
        var newServiceName = ConsoleHelper.GetInput("Enter new service name: ");
        var newAccountName = ConsoleHelper.GetInput("Enter new account name: ");
        var passwordLengthInput = ConsoleHelper.GetInput("Enter the number of characters for the new password (e.g. 10): ");

        if (!int.TryParse(passwordLengthInput, out var passwordLength))
        {
            ConsoleHelper.WriteError("Invalid input for password length. Ensure you enter a valid number.");
            return;
        }

        if (_vault.UpdatePasswordEntry(oldServiceName, oldAccountName, newServiceName, newAccountName, passwordLength))
        {
            ConsoleHelper.WriteSuccess($"Password entry for {oldServiceName}, {oldAccountName} has been updated with new details.");
        }
        else
        {
            ConsoleHelper.WriteError("Failed to update the password entry. Ensure the old service and account names are correct.");
        }
    }
}