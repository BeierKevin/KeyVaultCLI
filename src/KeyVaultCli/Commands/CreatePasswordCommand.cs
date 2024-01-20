using KeyVaultCli.Core;
using KeyVaultCli.UI;

namespace KeyVaultCli.Commands;

public class CreatePasswordCommand : ICommand
{
    private readonly Vault _vault;

    public CreatePasswordCommand(Vault vault)
    {
        _vault = vault;
    }

    public void Execute()
    {
        var serviceName = ConsoleHelper.GetInput("Enter service name for the new password: ");
        var accountName = ConsoleHelper.GetInput("Enter account name for the new password: ");
        var password = ConsoleHelper.GetInput("Enter the password: ");

        _vault.AddPasswordEntry(serviceName, accountName, password);

        ConsoleHelper.WriteSuccess($"A new password has been created and stored for {serviceName}, {accountName}.");
    }
}