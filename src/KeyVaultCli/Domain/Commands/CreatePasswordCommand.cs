using KeyVaultCli.Application;
using KeyVaultCli.Domain.Entities;

namespace KeyVaultCli.Domain.Commands;

public class CreatePasswordCommand : ICommand
{
    private readonly Vault _vault;
    private readonly IConsoleService _consoleService;

    public CreatePasswordCommand(Vault vault, IConsoleService consoleService)
    {
        _vault = vault;
        _consoleService = consoleService;
    }

    public void Execute()
    {
        var serviceName = _consoleService.GetInput("Enter service name for the new password: "); // Use the service
        var accountName = _consoleService.GetInput("Enter account name for the new password: "); // Use the service
        var password = _consoleService.GetInput("Enter the password: "); // Use the service

        _vault.AddPasswordEntry(serviceName, accountName, password);

        _consoleService.WriteSuccess($"A new password has been created and stored for {serviceName}, {accountName}."); // Use the service
    }
}