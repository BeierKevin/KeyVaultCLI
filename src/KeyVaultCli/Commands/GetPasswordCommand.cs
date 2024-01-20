using KeyVaultCli.Core;

namespace KeyVaultCli.Commands;

public class GetPasswordCommand : ICommand
{
    private readonly Vault _vault;

    public GetPasswordCommand(Vault vault)
    {
        _vault = vault;
    }

    public void Execute()
    {
        Console.Write("Enter the service name: ");
        var serviceName = Console.ReadLine();
            
        Console.Write("Enter the account name: ");
        var accountName = Console.ReadLine();

        var password = _vault.GetPassword(serviceName, accountName);

        if (!string.IsNullOrEmpty(password))
        {
            Console.WriteLine($"Password for {serviceName}, {accountName} is {password}.");
        }
        else
        {
            Console.WriteLine($"No password found for service {serviceName}, account {accountName}.");
        }
    }
}