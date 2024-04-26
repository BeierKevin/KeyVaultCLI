using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;
using System;

namespace KeyVaultCli.Application.Vault.Commands.RestoreVault
{
    public class RestoreVaultCommand : ICommand
    {
        private readonly IVault vault;
        private readonly IConsoleService consoleService;

        private readonly string restoreSuccessMsg = "  ____           _                     _ \n |  _ \\ ___  ___| |_ ___  _ __ ___  __| |\n | |_) / _ \\/ __| __/ _ \\| '__/ _ \\/ _` |\n |  _ <  __/\\__ \\ || (_) | | |  __/ (_| |\n |_| \\_\\___||___/\\__\\___/|_|  \\___|\\__,_|\n                                         ";

        private string confirmationPrompt = "Are you sure you want to restore the vault?";
        private string filePathPrompt = "Enter the full path to the backup file: ";

        private readonly string restoreErrorMsg = "Failed to restore vault.";
        private readonly string operationCancelMsg = "Operation cancelled.";

        public RestoreVaultCommand(IVault vault, IConsoleService consoleService)
        {
            this.vault = vault ?? throw new ArgumentNullException(nameof(vault));
            this.consoleService = consoleService ?? throw new ArgumentNullException(nameof(consoleService));
        }

        public void Execute()
        {
            try
            {
                if (consoleService.GetUserConfirmation(confirmationPrompt))
                {
                    PerformRestore();
                }
                else
                {
                    consoleService.WriteError(operationCancelMsg);
                }
            }
            catch (Exception ex)
            {
                consoleService.WriteError($"An error occurred during execute. Details: {ex.Message}");
            }
        }

        private void PerformRestore()
        {
            try
            {
                var backupFilePath = consoleService.GetInputFromPrompt(filePathPrompt);
                var success = vault.RestoreVault(backupFilePath);
                if (success)
                {
                    consoleService.WriteSuccess(restoreSuccessMsg);
                    consoleService.WriteSuccess("Restored all Passwords from backup, and added them to the vault.");
                }
                else
                {
                    consoleService.WriteError(restoreErrorMsg);
                }
            }
            catch (Exception ex)
            {
                consoleService.WriteError($"An error occurred during perform restore. Details: {ex.Message}");
            }
        }
    }
}