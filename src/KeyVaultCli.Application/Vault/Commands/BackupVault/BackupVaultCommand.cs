using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Application.Vault.Commands.BackupVault;

public class BackupVaultCommand(IVault vault, IConsole consoleService) : ICommand
{
    public void Execute()
    {
        var confirmation = consoleService.GetInputFromPrompt("Are you sure you want to backup the vault? (yes/no): ");
        if (confirmation.ToLower().Equals("yes"))
        {
            var backupFilePath = consoleService.GetInputFromPrompt("Enter the full path to the backup file: ");
            var success = vault.BackupVault(backupFilePath);
            if (success)
            {
                consoleService.WriteSuccess("Backed up Vault with all passwords in it!");
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