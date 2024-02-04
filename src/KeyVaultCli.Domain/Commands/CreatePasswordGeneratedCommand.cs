namespace KeyVaultCli.Domain.Commands;

public class CreatePasswordGenerateCommand : ICommand
{
    private readonly IVault _vault;
    private readonly IConsole _consoleService;

    public CreatePasswordGenerateCommand(IVault vault, IConsole consoleService)
    {
        _vault = vault;
        _consoleService = consoleService;
    }

    public void Execute()
    {
        var serviceName = _consoleService.GetInput("Enter service name for the new password: ");
        var accountName = _consoleService.GetInput("Enter account name for the new password: ");
        var passwordLengthStr = _consoleService.GetInput("Enter the desired password length: ");

        if (!int.TryParse(passwordLengthStr, out var passwordLength))
        {
            _consoleService.WriteError("Invalid input for password length. Ensure you enter a valid number.");
            return;
        }

        var password = _vault.GenerateAndAddPasswordEntry(serviceName, accountName, passwordLength);

        _consoleService.WriteSuccess($"A new password has been created and stored for {serviceName}, {accountName} with the value {password}.");
    }
}