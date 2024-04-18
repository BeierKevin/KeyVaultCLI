using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Application.Vault.Commands.BackupVault;

public class BackupVaultCommand(IVault vault, IConsole consoleService) : ICommand
{
    string backupSuccessMessage = "  ____             _                  ____                               __       _ \n | __ )  __ _  ___| | ___   _ _ __   / ___| _   _  ___ ___ ___  ___ ___ / _|_   _| |\n |  _ \\ / _` |/ __| |/ / | | | '_ \\  \\___ \\| | | |/ __/ __/ _ \\/ __/ __| |_| | | | |\n | |_) | (_| | (__|   <| |_| | |_) |  ___) | |_| | (_| (_|  __/\\__ \\__ \\  _| |_| | |\n |____/ \\__,_|\\___|_|\\_\\\\__,_| .__/  |____/ \\__,_|\\___\\___\\___||___/___/_|  \\__,_|_|\n                             |_|                                                    ";
    
    public void Execute()
    {
        var confirmation = consoleService.GetInputFromPrompt("Are you sure you want to backup the vault? (yes/no): ");
        if (confirmation.ToLower().Equals("yes"))
        {
            var backupFilePath = consoleService.GetInputFromPrompt("Enter the full path to the backup file: ");
            var success = vault.BackupVault(backupFilePath);
            if (success)
            {
                consoleService.WriteSuccess(backupSuccessMessage);
            }
            else
            {
                consoleService.WriteError("Failed to backup vault.");
            }
        }
        else
        {
            consoleService.WriteError("Operation cancelled.");
        }
    }
}