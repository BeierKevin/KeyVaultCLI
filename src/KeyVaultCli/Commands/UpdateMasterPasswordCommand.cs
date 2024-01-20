using KeyVaultCli.Core;

namespace KeyVaultCli.Commands;

public class UpdateMasterPasswordCommand : ICommand
{
    private readonly Vault _vault;

    public UpdateMasterPasswordCommand(Vault vault)
    {
        _vault = vault;
    }

    public void Execute()
    {
        Console.Write("Enter current master password: ");
        var oldPassword = Console.ReadLine();

        Console.Write("Enter new master password: ");
        var newPassword = Console.ReadLine();

        if (_vault.UpdateMasterPassword(oldPassword, newPassword))
        {
            Console.WriteLine("Master password has been updated.");
        }
        else
        {
            Console.WriteLine("Failed to update master password. Ensure the current master password is correct.");
        }
    }
}