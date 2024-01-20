using KeyVaultCli.UI;

namespace KeyVaultCli.Commands;

public class GetPasswordCommand : ICommand
{
    private readonly Vault _vault;

    public GetPasswordCommand(Vault vault)
    {
        _vault = vault;
    }

    public void Execute()
    {
        var serviceName = ConsoleHelper.GetInput("Enter the service name: ");
        var accountName = ConsoleHelper.GetInput("Enter the account name: ");

        var password = _vault.GetPassword(serviceName, accountName);

        if (!string.IsNullOrEmpty(password))
        {
            ConsoleHelper.WriteInfo($"Password for {serviceName}, {accountName} is {password}.");
        }
        else
        {
            ConsoleHelper.WriteError($"No password found for service {serviceName}, account {accountName}.");
        }
    }
}