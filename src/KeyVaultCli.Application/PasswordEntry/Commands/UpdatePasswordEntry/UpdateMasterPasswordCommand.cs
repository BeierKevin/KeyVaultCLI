using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Application.PasswordEntry.Commands.UpdatePasswordEntry;

public class UpdateMasterPasswordCommand(IVault vault, IConsole consoleService) : ICommand
{
    public void Execute()
    {
        var oldPassword = consoleService.GetInputFromPrompt("Enter current master password: ");
        var newPassword = consoleService.GetInputFromPrompt("Enter new master password: ");

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