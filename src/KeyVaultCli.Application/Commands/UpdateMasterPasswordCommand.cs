using KeyVaultCli.Domain;

namespace KeyVaultCli.Application.Commands;

public class UpdateMasterPasswordCommand(IVault vault, IConsole consoleService) : ICommand
{
    public void Execute()
    {
        var oldPassword = consoleService.GetInput("Enter current master password: ");
        var newPassword = consoleService.GetInput("Enter new master password: ");

        if (vault.UpdateMasterPassword(oldPassword, newPassword))
        {
            consoleService.WriteSuccess("Master password has been updated.");
        }
        else
        {
            consoleService.WriteError("No need to update you entered the same passwords.");
        }
    }
}