using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Application.PasswordEntry.Commands.CreatePasswordEntry;

public class CreatePasswordGenerateCommand(IVault vault, IConsole consoleService) : ICommand
{
    private readonly string serviceNamePrompt = "Enter service name for the new password: ";
    private readonly string accountNamePrompt = "Enter account name for the new password: ";
    private readonly string passwordLengthPrompt = "Enter the desired password length: ";
    private readonly string urlPrompt = "Enter the URL (leave empty if not applicable): ";
    private readonly string categoryPrompt = "Enter the category (leave empty if not applicable): ";
    private readonly string invalidLengthError = "Invalid input for password length. Ensure you enter a valid number.";
    private readonly string successMessage = "A new password has been created and stored for {0}, {1} with the value {2}.";

    public void Execute()
    {
        var serviceName = consoleService.GetInputFromPrompt(serviceNamePrompt);
        var accountName = consoleService.GetInputFromPrompt(accountNamePrompt);
        var passwordLength = GetPasswordLength();

        // If the password length is not valid, abort the operation.
        if (passwordLength < 1)
        {
            return;
        }

        var url = consoleService.GetInputFromPrompt(urlPrompt);
        var category = consoleService.GetInputFromPrompt(categoryPrompt);

        // Generate password internally and add a password entry
        var password = vault.GenerateAndAddPasswordEntry(serviceName, accountName, passwordLength, url, category);

        consoleService.WriteSuccess(string.Format(successMessage, serviceName, accountName, password));
    }
    
    private int GetPasswordLength()
    {
        var passwordLengthStr = consoleService.GetInputFromPrompt(passwordLengthPrompt);
        if (!int.TryParse(passwordLengthStr, out var passwordLength))
        {
            consoleService.WriteError(invalidLengthError);
            return -1;
        }
        return passwordLength;
    }
}