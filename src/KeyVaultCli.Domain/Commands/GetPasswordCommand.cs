namespace KeyVaultCli.Domain.Commands;

public class GetPasswordCommand : ICommand
{
    private readonly IVault _vault;
    private readonly IConsole _consoleService;

    public GetPasswordCommand(IVault vault, IConsole consoleService)
    {
        _vault = vault;
        _consoleService = consoleService;
    }

    public void Execute()
    {
        var serviceName = _consoleService.GetInput("Enter the service name: ");
        var accountName = _consoleService.GetInput("Enter the account name: ");

        var password = _vault.GetPassword(serviceName, accountName);

        if (!string.IsNullOrEmpty(password))
        {
            _consoleService.WriteInfo($"Password for {serviceName}, {accountName} is {password}.");
        }
        else
        {
            _consoleService.WriteError($"No password found for service {serviceName}, account {accountName}.");
        }
    }
}