using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;
using System;

namespace KeyVaultCli.Application.PasswordEntry.Commands.UpdatePasswordEntry
{
    public class UpdateMasterPasswordCommand(IVault vault, IConsoleService consoleService) : ICommand
    {
        private readonly IVault vault = vault ?? throw new ArgumentNullException(nameof(vault));
        private readonly IConsoleService consoleService = consoleService ?? throw new ArgumentNullException(nameof(consoleService));

        private readonly string oldPasswordPrompt = "Enter current master password: ";
        private readonly string newPasswordPrompt = "Enter new master password: ";
        private readonly string successMessage = "Master password has been updated.";
        private readonly string errorMessage = "No need to update you entered the same passwords.";

        public void Execute()
        {
            try
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
            catch(Exception ex)
            {
                consoleService.WriteError($"An error occurred while trying to update the master password. Details: {ex.Message}");
            }
        }
    }
}