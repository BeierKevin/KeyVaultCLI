using KeyVaultCli.Domain;

namespace KeyVaultCli.Application.Commands;

public class CreatePasswordCommand(IVault vault, IConsole consoleService) : ICommand
{
    public void Execute()
    {
        var serviceName = consoleService.GetInput("Enter service name for the new password: "); // Use the service
        var accountName = consoleService.GetInput("Enter account name for the new password: "); // Use the service
        var password = consoleService.GetInput("Enter the password: "); // Use the service

        vault.AddPasswordEntry(serviceName, accountName, password);

        consoleService.WriteSuccess($"A new password has been created and stored for {serviceName}, {accountName}."); // Use the service
    }
}