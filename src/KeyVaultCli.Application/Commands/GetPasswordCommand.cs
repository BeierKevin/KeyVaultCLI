using KeyVaultCli.Domain;

namespace KeyVaultCli.Application.Commands;

public class GetPasswordCommand(IVault vault, IConsole consoleService) : ICommand
{
    public void Execute()
    {
        var serviceName = consoleService.GetInput("Enter the service name: ");
        var accountName = consoleService.GetInput("Enter the account name: ");

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