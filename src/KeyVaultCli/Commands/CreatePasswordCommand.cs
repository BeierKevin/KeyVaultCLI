using KeyVaultCli.Core;

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
        Console.Write("Enter service name: ");
        var serviceName = Console.ReadLine();

        Console.Write("Enter account name: ");
        var accountName = Console.ReadLine();

        Console.Write("Enter password: ");
        var password = Console.ReadLine();

        if (string.IsNullOrEmpty(serviceName) || string.IsNullOrEmpty(accountName) || string.IsNullOrEmpty(password))
        {
            Console.WriteLine("Service Name, Account Name and Password cannot be empty.");
            return;
        }

        _vault.AddPasswordEntry(serviceName, accountName, password);
        Console.WriteLine("The password entry has been added.");
    }
}