using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain;
using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Application.PasswordEntry.Commands.DeletePasswordEntry;

public class DeleteAllPasswordsCommand(IVault vault, IConsole consoleService) : ICommand
{
    public void Execute()
    {
        var confirmation = consoleService.GetInputFromPrompt("Are you sure you want to delete all passwords? (yes/no): ");
        if (confirmation.ToLower() == "yes")
        {
            vault.DeleteAllPasswordEntries();
            consoleService.WriteSuccess("All passwords have been deleted.");
        }
        else
        {
            consoleService.WriteError("Operation cancelled.");
        }
    }
}