using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Application.Vault.Commands.DeleteVault;

public class DeleteVaultCommand(IVaultFactory vault, IConsole consoleService) : ICommand
{
    private readonly string confirmationPrompt = "Are you sure you want to delete the vault?";
    private readonly string deleteSuccessMsg = "Deleted Vault with all passwords in it!";
    private readonly string deleteErrMsg = "Closing application, to create a new vault run the application again.";
    private readonly string operationCancelMsg = "Operation cancelled.";

    public void Execute()
    {
        if (consoleService.GetUserConfirmation(confirmationPrompt))
        {
            vault.DeleteVault();
            consoleService.WriteSuccess(deleteSuccessMsg);
            consoleService.WriteError(deleteErrMsg);
            Environment.Exit(-1);
        }
        else
        {
            consoleService.WriteError(operationCancelMsg);
        }
    }
}