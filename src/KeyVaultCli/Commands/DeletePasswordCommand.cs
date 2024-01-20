using KeyVaultCli.Core;

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
        Console.Write("Enter the service name: ");
        var serviceName = Console.ReadLine();
            
        Console.Write("Enter the account name: ");
        var accountName = Console.ReadLine();

        if (_vault.DeletePasswordEntry(serviceName, accountName))
        {
            Console.WriteLine($"Password for {serviceName}, {accountName} has been deleted.");
        }
        else
        {
            Console.WriteLine($"No password found for service {serviceName}, account {accountName}.");
        }
    }
}