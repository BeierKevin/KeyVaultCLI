using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;
using KeyVaultCli.Domain.Services.PasswordHealthCheck;

namespace KeyVaultCli.Application.PasswordEntry.Commands.GetPasswordEntry;

public class GetPasswordCommand : ICommand
{
    private readonly IVault vault;
    private readonly IConsole consoleService;
    private readonly PasswordHealthService passwordHealthService;

    public GetPasswordCommand(IVault vault, IConsole consoleService)
    {
        this.vault = vault;
        this.consoleService = consoleService;
        this.passwordHealthService = new PasswordHealthService(
            new PasswordStrengthService(),
            new PasswordUniquenessService(this.vault),
            new CompromisedPasswordService());
    }

    public void Execute()
    {
        var serviceName = consoleService.GetInputFromPrompt("Enter the service name: ");
        var accountName = consoleService.GetInputFromPrompt("Enter the account name: ");

        if (accountName == string.Empty || serviceName == string.Empty)
        {
            consoleService.WriteError("Service name and account name cannot be empty.");
            return;
        }

        var passwordEntry = vault.GetPasswordEntry(serviceName, accountName);

        if (passwordEntry != null)
        {
            var decryptedPassword = vault.GetPassword(serviceName, accountName);
            var passwordHealthResult = passwordHealthService.CheckPasswordHealthAsync(decryptedPassword).Result;

            consoleService.WriteInfo($"Information for {serviceName}, {accountName}:");
            consoleService.WriteInfo($"Password: {decryptedPassword}");
            consoleService.WriteInfo($"URL: {passwordEntry.Url}");
            consoleService.WriteInfo($"Category: {passwordEntry.Category}");

            if (passwordHealthResult.IsStrong && passwordHealthResult.IsUnique && !passwordHealthResult.IsCompromised)
            {
                consoleService.WriteInfo("This password is healthy.");
            }
            else
            {
                consoleService.WriteWarning(
                    "This password is not healthy. A healthy password should be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, one digit and be unique inside this Vault.");
            }
        }
        else
        {
            consoleService.WriteError($"No password entry found for service {serviceName}, account {accountName}.");
        }
    }
}