using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Application.PasswordEntry.Commands.CreatePasswordEntry;

public class CreatePasswordCommand(IVault vault, IConsole consoleService) : ICommand
{
    private readonly string serviceNamePrompt = "Enter service name for the new password: ";
    private readonly string accountNamePrompt = "Enter account name for the new password: ";
    private readonly string passwordPrompt = "Enter the password: ";
    private readonly string urlPrompt = "Enter the URL (leave empty if not applicable): ";
    private readonly string categoryPrompt = "Enter the category (leave empty if not applicable): ";
    private readonly string successMessage = "A new password has been created and stored for {0}, {1}.";

    public void Execute()
    {
        var serviceName = consoleService.GetInputFromPrompt(serviceNamePrompt);
        var accountName = consoleService.GetInputFromPrompt(accountNamePrompt);
        var password = consoleService.GetInputFromPrompt(passwordPrompt);
        var url = consoleService.GetInputFromPrompt(urlPrompt);
        var category = consoleService.GetInputFromPrompt(categoryPrompt);

        vault.AddPasswordEntry(serviceName, accountName, password, url, category);

        consoleService.WriteSuccess(string.Format(successMessage, serviceName, accountName));
    }
}