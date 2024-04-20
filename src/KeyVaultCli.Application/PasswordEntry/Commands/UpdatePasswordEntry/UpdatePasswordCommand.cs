using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Application.PasswordEntry.Commands.UpdatePasswordEntry;

public class UpdatePasswordCommand(IVault vault, IConsole consoleService) : ICommand
{
    private readonly string currentServiceNamePrompt = "Enter current service name: ";
    private readonly string currentAccountNamePrompt = "Enter current account name: ";
    private readonly string newServiceNamePrompt = "Enter new service name: ";
    private readonly string newAccountNamePrompt = "Enter new account name: ";
    private readonly string newPasswordPrompt = "Enter new password: ";
    private readonly string newUrlPrompt = "Enter new URL: ";
    private readonly string newCategoryPrompt = "Enter new Category: ";
    private readonly string successMessage = "The password entry has been updated.";
    private readonly string errorMessage = "Failed to update the password entry. Ensure the service and account exists.";

    public void Execute()
    {
        var currentServiceName = consoleService.GetInputFromPrompt(currentServiceNamePrompt);
        var currentAccountName = consoleService.GetInputFromPrompt(currentAccountNamePrompt);
        var newServiceName = consoleService.GetInputFromPrompt(newServiceNamePrompt);
        var newAccountName = consoleService.GetInputFromPrompt(newAccountNamePrompt);
        var newPassword = consoleService.GetInputFromPrompt(newPasswordPrompt);
        var newUrl = consoleService.GetInputFromPrompt(newUrlPrompt);
        var newCategory = consoleService.GetInputFromPrompt(newCategoryPrompt);

        if (vault.UpdatePasswordEntry(currentServiceName, currentAccountName, newServiceName, newAccountName, newPassword.Length, newPassword, newUrl, newCategory))
        {
            consoleService.WriteSuccess(successMessage);
        }
        else
        {
            consoleService.WriteError(errorMessage);
        }
    }
}