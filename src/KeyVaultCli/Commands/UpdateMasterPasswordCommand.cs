using KeyVaultCli.Core;
using KeyVaultCli.UI;

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
        var oldPassword = ConsoleHelper.GetInput("Enter current master password: ");
        var newPassword = ConsoleHelper.GetInput("Enter new master password: ");

        if (_vault.UpdateMasterPassword(oldPassword, newPassword))
        {
            ConsoleHelper.WriteSuccess("Master password has been updated.");
        }
        else
        {
            ConsoleHelper.WriteError("No need to update you entered the same passwords.");
        }
    }
}