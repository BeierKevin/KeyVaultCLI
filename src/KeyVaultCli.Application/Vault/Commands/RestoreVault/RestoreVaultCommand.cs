using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Application.Vault.Commands.RestoreVault;

public class RestoreVaultCommand(IVault vault, IConsole consoleService) : ICommand
{
    public void Execute()
    {
        var confirmation = consoleService.GetInputFromPrompt("Are you sure you want to restore the vault? (yes/no): ");
        if (confirmation.ToLower().Equals("yes"))
        {
            var backupFilePath = consoleService.GetInputFromPrompt("Enter the full path to the backup file: ");
            var success = vault.RestoreVault(backupFilePath);
            if (success)
            {
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