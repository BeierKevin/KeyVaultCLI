using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Application.PasswordEntry.Commands.GetPasswordEntry;

public class GetPasswordCommand(IVault vault, IConsole consoleService) : ICommand
{
    public void Execute()
    {
        var serviceName = consoleService.GetInputFromPrompt("Enter the service name: ");
        var accountName = consoleService.GetInputFromPrompt("Enter the account name: ");

        var password = vault.GetPassword(serviceName, accountName);

        if (!string.IsNullOrEmpty(password))
        {
            consoleService.WriteInfo($"Password for {serviceName}, {accountName} is {password}.");
        }
        else
        {
            consoleService.WriteError($"No password found for service {serviceName}, account {accountName}.");
        }
    }
}