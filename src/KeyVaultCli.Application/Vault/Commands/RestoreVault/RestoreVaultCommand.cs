using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Application.Vault.Commands.RestoreVault;

public class RestoreVaultCommand(IVault vault, IConsole consoleService) : ICommand
{
    string restroedSuccessMessage = "  ____           _                     _ \n |  _ \\ ___  ___| |_ ___  _ __ ___  __| |\n | |_) / _ \\/ __| __/ _ \\| '__/ _ \\/ _` |\n |  _ <  __/\\__ \\ || (_) | | |  __/ (_| |\n |_| \\_\\___||___/\\__\\___/|_|  \\___|\\__,_|\n                                         ";
    
    public void Execute()
    {
        var confirmation = consoleService.GetInputFromPrompt("Are you sure you want to restore the vault? (yes/no): ");
        if (confirmation.ToLower().Equals("yes"))
        {
            var backupFilePath = consoleService.GetInputFromPrompt("Enter the full path to the backup file: ");
            var success = vault.RestoreVault(backupFilePath);
            if (success)
            {
                consoleService.WriteSuccess(restroedSuccessMessage);
                consoleService.WriteSuccess("Restored all Passwords from backup, and added them to the vault.");
            }
            else
            {
                consoleService.WriteError("Failed to restore vault.");
            }
        }
        else
        {
            consoleService.WriteError("Operation cancelled.");
        }
    }
}