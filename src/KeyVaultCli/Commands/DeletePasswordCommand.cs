using KeyVaultCli.Core;
using KeyVaultCli.UI;

namespace KeyVaultCli.Commands;

public class DeletePasswordCommand : ICommand
{
    private readonly Vault _vault;

    public DeletePasswordCommand(Vault vault)
    {
        _vault = vault;
    }

    public void Execute()
    {
        var serviceName = ConsoleHelper.GetInput("Enter the service name for the password you want to delete: ");
        var accountName = ConsoleHelper.GetInput("Enter the account name for the password you want to delete: ");

        var isDeleted = _vault.DeletePasswordEntry(serviceName, accountName);

        if (isDeleted)
        {
            ConsoleHelper.WriteSuccess("Password entry has been deleted.");
        }
        else
        {
            ConsoleHelper.WriteError("Failed to delete the password entry. Ensure the service and account names are correct.");
        }
    }
}