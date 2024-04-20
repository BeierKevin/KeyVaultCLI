using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Application.PasswordEntry.Commands.DeletePasswordEntry;

public class DeleteAllPasswordsCommand(IVault vault, IConsole consoleService) : ICommand
{
    private readonly string confirmationPrompt = "Are you sure you want to delete all passwords?";
    private readonly string successMessage = "All passwords have been deleted.";
    private readonly string errorMessage = "Operation cancelled.";

    public void Execute()
    {
        var confirmation = consoleService.GetUserConfirmation(confirmationPrompt);
        if (confirmation)
        {
            vault.DeleteAllPasswordEntries();
            consoleService.WriteSuccess(successMessage);
        }
        else
        {
            consoleService.WriteError(errorMessage);
        }
    }
}