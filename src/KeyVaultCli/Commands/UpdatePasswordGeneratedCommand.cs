using KeyVaultCli.Core;

namespace KeyVaultCli.Commands;

public class UpdatePasswordGeneratedCommand : ICommand
{
    private readonly Vault _vault;

    public UpdatePasswordGeneratedCommand(Vault vault)
    {
        _vault = vault;
    }

    public void Execute()
    {
        Console.Write("Enter old service name: ");
        var oldServiceName = Console.ReadLine();

        Console.Write("Enter old account name: ");
        var oldAccountName = Console.ReadLine();

        Console.Write("Enter new service name: ");
        var newServiceName = Console.ReadLine();

        Console.Write("Enter new account name: ");
        var newAccountName = Console.ReadLine();

        Console.Write("Enter the number of characters for the new password (e.g. 10): ");
        var passwordLengthInput = Console.ReadLine();

        if (!int.TryParse(passwordLengthInput, out int passwordLength))
        {
            Console.WriteLine("Invalid input for password length. Ensure you enter a valid number.");
            return;
        }

        if (_vault.UpdatePasswordEntry(oldServiceName, oldAccountName, newServiceName, newAccountName, 
            int.Parse(passwordLengthInput)))
        {
            Console.WriteLine($"Password entry for {oldServiceName}, {oldAccountName} has been updated with new details.");
        }
        else
        {
            Console.WriteLine("Failed to update the password entry. Ensure the old service and account names are correct.");
        }
    }
}