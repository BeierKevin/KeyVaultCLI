using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Application.Vault.Commands.RestoreVault;

public class RestoreVaultCommand(IVault vault, IConsole consoleService) : ICommand
{
    private readonly string restoreSuccessMsg = "  ____           _                     _ \n |  _ \\ ___  ___| |_ ___  _ __ ___  __| |\n | |_) / _ \\/ __| __/ _ \\| '__/ _ \\/ _` |\n |  _ <  __/\\__ \\ || (_) | | |  __/ (_| |\n |_| \\_\\___||___/\\__\\___/|_|  \\___|\\__,_|\n                                         ";

    private string confirmationPrompt = "Are you sure you want to restore the vault?";
    private string filePathPrompt = "Enter the full path to the backup file: ";

    private readonly string restoreErrorMsg = "Failed to restore vault.";
    private readonly string operationCancelMsg = "Operation cancelled.";

    public void Execute()
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

    private void PerformRestore()
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
}