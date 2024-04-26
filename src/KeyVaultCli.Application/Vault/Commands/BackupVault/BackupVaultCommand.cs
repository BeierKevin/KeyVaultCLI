using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;
using System;

namespace KeyVaultCli.Application.Vault.Commands.BackupVault
{
    public class BackupVaultCommand(IVault vault, IConsoleService consoleService) : ICommand
    {
        private readonly IVault vault = vault ?? throw new ArgumentNullException(nameof(vault));
        private readonly IConsoleService consoleService = consoleService ?? throw new ArgumentNullException(nameof(consoleService));

        private readonly string backupSuccessMsg = "  ____             _                  ____                               __       _ \n | __ )  __ _  ___| | ___   _ _ __   / ___| _   _  ___ ___ ___  ___ ___ / _|_   _| |\n |  _ \\ / _` |/ __| |/ / | | | '_ \\  \\___ \\| | | |/ __/ __/ _ \\/ __/ __| |_| | | | |\n | |_) | (_| | (__|   <| |_| | |_) |  ___) | |_| | (_| (_|  __/\\__ \\__ \\  _| |_| | |\n |____/ \\__,_|\\___|_|\\_\\\\__,_| .__/  |____/ \\__,_|\\___\\___\\___||___/___/_|  \\__,_|_|\n                             |_|                                                    ";
        private readonly string confirmationPrompt = "Are you sure you want to backup the vault?";
        private readonly string filePathPrompt = "Enter the full path to the backup file: ";
        private readonly string backupErrMsg = "Failed to backup vault.";
        private readonly string operationCancelMsg = "Operation cancelled.";

        public void Execute()
        {
            try 
            {
                if (consoleService.GetUserConfirmation(confirmationPrompt))
                {
                    PerformBackup();
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

        private void PerformBackup()
        {
            try 
            {
                var backupFilePath = consoleService.GetInputFromPrompt(filePathPrompt);
                var success = vault.BackupVault(backupFilePath);
                if (success)
                {
                    consoleService.WriteSuccess(backupSuccessMsg);
                }
                else
                {
                    consoleService.WriteError(backupErrMsg);
                }
            }
            catch (Exception ex)
            {
                consoleService.WriteError($"An error occurred during perform backup. Details: {ex.Message}");
            }
        }
    }
}