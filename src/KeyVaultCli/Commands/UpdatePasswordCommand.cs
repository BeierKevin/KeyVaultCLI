using KeyVaultCli.UI;

namespace KeyVaultCli.Commands;

public class UpdatePasswordCommand : ICommand
{
    private readonly Vault _vault;

    public UpdatePasswordCommand(Vault vault)
    {
        _vault = vault;
    }

    public void Execute()
    {
        var currentServiceName = ConsoleHelper.GetInput("Enter current service name: ");
        var currentAccountName = ConsoleHelper.GetInput("Enter current account name: ");
        var newServiceName = ConsoleHelper.GetInput("Enter new service name: ");
        var newAccountName = ConsoleHelper.GetInput("Enter new account name: ");
        var newPassword = ConsoleHelper.GetInput("Enter new password: ");

        if (_vault.UpdatePasswordEntry(currentServiceName, currentAccountName, newServiceName, newAccountName, newPassword.Length, newPassword))
        {
            ConsoleHelper.WriteSuccess("The password entry has been updated.");
        }
        else
        {
            ConsoleHelper.WriteError("Failed to update the password entry. Ensure the service and account exists.");
        }
    }
}