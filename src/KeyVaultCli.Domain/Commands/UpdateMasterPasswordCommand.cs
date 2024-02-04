namespace KeyVaultCli.Domain.Commands;

public class UpdateMasterPasswordCommand : ICommand
{
    private readonly IVault _vault;
    private readonly IConsole _consoleService;

    public UpdateMasterPasswordCommand(IVault vault, IConsole consoleService)
    {
        _vault = vault;
        _consoleService = consoleService;
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