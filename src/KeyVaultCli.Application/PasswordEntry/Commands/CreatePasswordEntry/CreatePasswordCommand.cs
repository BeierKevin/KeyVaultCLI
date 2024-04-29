using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Application.PasswordEntry.Commands.CreatePasswordEntry;

public class CreatePasswordCommand(IVault vault, IConsoleService consoleService) : ICommand
{
    private readonly IVault vault = vault ?? throw new ArgumentNullException(nameof(vault));

    private readonly IConsole consoleService =
        consoleService ?? throw new ArgumentNullException(nameof(consoleService));

    private const string serviceNamePrompt = "Enter service name for the new password: ";
    private const string accountNamePrompt = "Enter account name for the new password: ";
    private const string passwordPrompt = "Enter the password: ";
    private const string urlPrompt = "Enter the URL (optional): ";
    private const string categoryPrompt = "Enter the category (optional): ";
    private const string successMessage = "A new password has been created and stored for {0}, {1}.";

    public void Execute()
    {
        try
        {
            var serviceName = consoleService.GetInputFromPrompt(serviceNamePrompt);
            if (string.IsNullOrWhiteSpace(serviceName))
            {
                consoleService.WriteError("Service name must not be empty.");
                return;
            }

            var accountName = consoleService.GetInputFromPrompt(accountNamePrompt);
            if (string.IsNullOrWhiteSpace(accountName))
            {
                consoleService.WriteError("Account name must not be empty.");
                return;
            }

            var password = consoleService.GetInputFromPrompt(passwordPrompt);
            if (string.IsNullOrWhiteSpace(password))
            {
                consoleService.WriteError("Password must not be empty.");
                return;
            }

            var url = consoleService.GetInputFromPrompt(urlPrompt);
            var category = consoleService.GetInputFromPrompt(categoryPrompt);

            vault.AddEntryToPasswordList(serviceName, accountName, password, url, category);

            consoleService.WriteSuccess(string.Format(successMessage, serviceName, accountName));
        }
        catch (Exception ex)
        {
            // Handle or log exception
            consoleService.WriteError("An error occurred while trying to create a password entry. Details: " +
                                      ex.Message);
        }
    }
}