using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Application.PasswordEntry.Commands.DeletePasswordEntry;

public class DeletePasswordCommand(IVault vault, IConsoleService consoleService) : ICommand
{
    private readonly IVault vault = vault ?? throw new ArgumentNullException(nameof(vault));
    private readonly IConsoleService consoleService = consoleService ?? throw new ArgumentNullException(nameof(consoleService));

    private readonly string serviceNamePrompt = "Enter the service name for the password you want to delete: ";
    private readonly string accountNamePrompt = "Enter the account name for the password you want to delete: ";
    private readonly string successMessage = "Password entry has been deleted.";
    private readonly string errorMessage = "Failed to delete the password entry. Ensure the service and account names are correct.";

    public void Execute()
    {
        try
        {
            var serviceName = GetServiceName();
            if (string.IsNullOrWhiteSpace(serviceName))
            {
                consoleService.WriteError("Service name must not be empty.");
                return;
            }

            var accountName = GetAccountName();
            if (string.IsNullOrWhiteSpace(accountName))
            {
                consoleService.WriteError("Account name must not be empty.");
                return;
            }

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
        catch(Exception ex)
        {
            // Handle or log the precise error message
            consoleService.WriteError("An error occurred while trying to delete a password entry. Details: " + ex.Message);
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