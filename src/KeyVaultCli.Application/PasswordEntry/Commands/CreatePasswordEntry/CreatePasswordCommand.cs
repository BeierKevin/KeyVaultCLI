using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain;
using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Application.PasswordEntry.Commands.CreatePasswordEntry;

public class CreatePasswordCommand(IVault vault, IConsole consoleService) : ICommand
{
    public void Execute()
    {
        var serviceName = consoleService.GetInputFromPrompt("Enter service name for the new password: "); // Use the service
        var accountName = consoleService.GetInputFromPrompt("Enter account name for the new password: "); // Use the service
        var password = consoleService.GetInputFromPrompt("Enter the password: "); // Use the service

        vault.AddPasswordEntry(serviceName, accountName, password);

        consoleService.WriteSuccess($"A new password has been created and stored for {serviceName}, {accountName}."); // Use the service
    }
}