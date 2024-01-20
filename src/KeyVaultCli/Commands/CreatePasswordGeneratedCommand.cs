using KeyVaultCli.UI;

namespace KeyVaultCli.Commands;

public class CreatePasswordGenerateCommand : ICommand
{
    private readonly Vault _vault;

    public CreatePasswordGenerateCommand(Vault vault)
    {
        _vault = vault;
    }

    public void Execute()
    {
        var serviceName = ConsoleHelper.GetInput("Enter service name for the new password: ");
        var accountName = ConsoleHelper.GetInput("Enter account name for the new password: ");
        var passwordLengthStr = ConsoleHelper.GetInput("Enter the desired password length: ");

        if (!int.TryParse(passwordLengthStr, out var passwordLength))
        {
            ConsoleHelper.WriteError("Invalid input for password length. Ensure you enter a valid number.");
            return;
        }

        var password = _vault.GenerateAndAddPasswordEntry(serviceName, accountName, passwordLength);

        ConsoleHelper.WriteSuccess($"A new password has been created and stored for {serviceName}, {accountName} with the value {password}.");
    }
}