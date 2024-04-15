using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Application.PasswordEntry.Commands.GetPasswordEntry;

public class GetPasswordCommand(IVault vault, IConsole consoleService) : ICommand
{
    public void Execute()
{
    var serviceName = consoleService.GetInputFromPrompt("Enter the service name: ");
    var accountName = consoleService.GetInputFromPrompt("Enter the account name: ");
    
    if(accountName == string.Empty || serviceName == string.Empty)
    {
        consoleService.WriteError("Service name and account name cannot be empty.");
        return;
    }

    var passwordEntry = vault.GetPasswordEntry(serviceName, accountName);

    if (passwordEntry != null)
    {
        consoleService.WriteInfo($"Information for {serviceName}, {accountName}:");
        consoleService.WriteInfo($"Password: {passwordEntry.EncryptedPassword}");
        consoleService.WriteInfo($"URL: {passwordEntry.Url}");
        consoleService.WriteInfo($"Category: {passwordEntry.Category}");
    }
    else
    {
        consoleService.WriteError($"No password entry found for service {serviceName}, account {accountName}.");
    }
}
}