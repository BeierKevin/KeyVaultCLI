using KeyVaultCli.Core;

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
        Console.Write("Enter service name for the new password: ");
        var serviceName = Console.ReadLine();

        Console.Write("Enter account name for the new password: ");
        var accountName = Console.ReadLine();

        Console.Write("Enter the desired password length (e.g. 10): ");
        var passwordLengthInput = Console.ReadLine();

        if (!int.TryParse(passwordLengthInput, out var passwordLength))
        {
            Console.WriteLine("Invalid input for password length. Ensure you enter a valid number.");
            return;
        }

        var password = _vault.GenerateAndAddPasswordEntry(serviceName, accountName, passwordLength);

        Console.WriteLine($"A new password has been generated and stored for {serviceName}, {accountName}, with the value: {password}");
    }
}