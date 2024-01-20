using KeyVaultCli.Core;

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
        Console.Write("Enter current service name: ");
        var currentServiceName = Console.ReadLine();

        Console.Write("Enter current account name: ");
        var currentAccountName = Console.ReadLine();

        Console.Write("Enter new service name: ");
        var newServiceName = Console.ReadLine();

        Console.Write("Enter new account name: ");
        var newAccountName = Console.ReadLine();

        Console.Write("Enter new password: ");
        var newPassword = Console.ReadLine();

        if (_vault.UpdatePasswordEntry(currentServiceName, currentAccountName, newServiceName, newAccountName, newPassword.Length, newPassword))
        {
            Console.WriteLine("The password entry has been updated.");
        }
        else
        {
            Console.WriteLine("Failed to update the password entry. Ensure the service and account exists.");
        }
    }
}