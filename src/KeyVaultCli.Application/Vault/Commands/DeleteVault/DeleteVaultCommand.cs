using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Application.Vault.Commands.DeleteVault;

public class DeleteVaultCommand(IVaultFactory vault, IConsole consoleService) : ICommand
{
    public void Execute()
    {
        var confirmation = consoleService.GetInputFromPrompt("Are you sure you want to delete all passwords? (yes/no): ");
        if (confirmation.ToLower() == "yes")
        {
            vault.DeleteVault();
            consoleService.WriteSuccess("Deleted Vault with all passwords in it!");
            consoleService.WriteError("Closing application, to create a new vault run the application again.");
            Environment.Exit(-1);
        }
        else
        {
            consoleService.WriteError("Operation cancelled.");
        }
    }
}
