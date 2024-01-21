using KeyVaultCli.Application;
using KeyVaultCli.Core;
using KeyVaultCli.Infrastructure.UI;

namespace KeyVaultCli.Commands;

public class UpdateMasterPasswordCommand : ICommand
{
    private readonly Vault _vault;
    private readonly IConsoleService _consoleService;

    public UpdateMasterPasswordCommand(Vault vault, IConsoleService consoleService)
    {
        _vault = vault;
        _consoleService = new ConsoleService();
    }

    public void Execute()
    {
        var oldPassword = _consoleService.GetInput("Enter current master password: ");
        var newPassword = _consoleService.GetInput("Enter new master password: ");

        if (_vault.UpdateMasterPassword(oldPassword, newPassword))
        {
            _consoleService.WriteSuccess("Master password has been updated.");
        }
        else
        {
            _consoleService.WriteError("No need to update you entered the same passwords.");
        }
    }
}