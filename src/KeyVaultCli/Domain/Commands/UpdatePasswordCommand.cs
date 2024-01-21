using KeyVaultCli.Application;
using KeyVaultCli.Domain.Entities;
using KeyVaultCli.Infrastructure;

namespace KeyVaultCli.Domain.Commands;

public class UpdatePasswordCommand : ICommand
{
    private readonly Vault _vault;
    private readonly IConsoleService _consoleService;

    public UpdatePasswordCommand(Vault vault, IConsoleService consoleService)
    {
        _vault = vault;
        _consoleService = new ConsoleService();
    }

    public void Execute()
    {
        var currentServiceName = _consoleService.GetInput("Enter current service name: ");
        var currentAccountName = _consoleService.GetInput("Enter current account name: ");
        var newServiceName = _consoleService.GetInput("Enter new service name: ");
        var newAccountName = _consoleService.GetInput("Enter new account name: ");
        var newPassword = _consoleService.GetInput("Enter new password: ");

        if (_vault.UpdatePasswordEntry(currentServiceName, currentAccountName, newServiceName, newAccountName, newPassword.Length, newPassword))
        {
            _consoleService.WriteSuccess("The password entry has been updated.");
        }
        else
        {
            _consoleService.WriteError("Failed to update the password entry. Ensure the service and account exists.");
        }
    }
}