using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Application.PasswordEntry.Commands.UpdatePasswordEntry;

public class UpdateMasterPasswordCommand(IVault vault, IConsole consoleService) : ICommand
{
    private readonly string oldPasswordPrompt = "Enter current master password: ";
    private readonly string newPasswordPrompt = "Enter new master password: ";
    private readonly string successMessage = "Master password has been updated.";
    private readonly string errorMessage = "No need to update you entered the same passwords.";

    public void Execute()
    {
        var oldPassword = consoleService.GetInputFromPrompt(oldPasswordPrompt);
        var newPassword = consoleService.GetInputFromPrompt(newPasswordPrompt);

        if (vault.UpdateMasterPassword(oldPassword, newPassword))
        {
            consoleService.WriteSuccess(successMessage);
        }
        else
        {
            consoleService.WriteError(errorMessage);
        }
    }
}