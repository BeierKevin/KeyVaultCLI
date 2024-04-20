using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Application.PasswordEntry.Commands.DeletePasswordEntry;

public class DeletePasswordCommand(IVault vault, IConsole consoleService) : ICommand
{
    private readonly string serviceNamePrompt = "Enter the service name for the password you want to delete: ";
    private readonly string accountNamePrompt = "Enter the account name for the password you want to delete: ";
    private readonly string successMessage = "Password entry has been deleted.";
    private readonly string errorMessage = "Failed to delete the password entry. Ensure the service and account names are correct.";

    public void Execute()
    {
        var serviceName = GetServiceName();
        var accountName = GetAccountName();

        var isDeleted = vault.DeletePasswordEntry(serviceName, accountName);

        if (isDeleted)
        {
            consoleService.WriteSuccess(successMessage);
        }
        else
        {
            consoleService.WriteError(errorMessage);
        }
    }
    
    private string GetServiceName()
    {
        return consoleService.GetInputFromPrompt(serviceNamePrompt);
    }
    
    private string GetAccountName()
    {
        return consoleService.GetInputFromPrompt(accountNamePrompt);
    }
}