using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;
using KeyVaultCli.Domain.Services.PasswordHealthCheck;
using System;

namespace KeyVaultCli.Application.PasswordEntry.Commands.GetPasswordEntry
{
    public class GetPasswordCommand : ICommand
    {
        private readonly IVault vault;
        private readonly IConsoleService consoleService;
        private readonly PasswordHealthService passwordHealthService;

        private readonly string serviceNamePrompt = "Enter the service name: ";
        private readonly string accountNamePrompt = "Enter the account name: ";
        private readonly string errorEmptyInputMessage = "Service name and account name cannot be empty.";
        private readonly string passwordInfoMessage = "Information for {0}, {1}:";
        private readonly string passwordNotFoundMessage = "No password entry found for service {0}, account {1}.";

        private readonly string warningMessage =
            "This password is not healthy. A healthy password should be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, one digit and be unique inside this Vault.";

        private readonly string passwordHealthyMessage = "This password is healthy.";

        public GetPasswordCommand(IVault vault, IConsoleService consoleService)
        {
            this.vault = vault ?? throw new ArgumentNullException(nameof(vault));
            this.consoleService = consoleService ?? throw new ArgumentNullException(nameof(consoleService));
            this.passwordHealthService = new PasswordHealthService(
                new PasswordStrengthService(),
                new PasswordUniquenessService(this.vault),
                new CompromisedPasswordService());
        }

        public void Execute()
        {
            try
            {
                var serviceName = GetServiceName();
                var accountName = GetAccountName();

                if (accountName == string.Empty || serviceName == string.Empty)
                {
                    consoleService.WriteError(errorEmptyInputMessage);
                    return;
                }

                var passwordEntry = vault.GetPasswordEntry(serviceName, accountName);

                if (passwordEntry != null)
                {
                    WritePasswordInfo(passwordEntry, serviceName, accountName);
                }
                else
                {
                    consoleService.WriteError(string.Format(passwordNotFoundMessage, serviceName, accountName));
                }
            }
            catch (Exception ex)
            {
                consoleService.WriteError(
                    $"An error occurred while trying to execute the command. Details: {ex.Message}");
            }
        }

        private string GetServiceName()
        {
            return consoleService.GetInputFromPrompt(serviceNamePrompt);
        }

        private string GetAccountName()
        {
            return consoleService.GetInputFromPrompt(accountNamePrompt);
        }

        private void WritePasswordInfo(Domain.Entities.PasswordEntry passwordEntry, string serviceName,
            string accountName)
        {
            var decryptedPassword = vault.DecryptAndRetrievePassword(serviceName, accountName);
            var passwordHealthResult = passwordHealthService.CheckPasswordHealthAsync(decryptedPassword).Result;

            consoleService.WriteInfo(string.Format(passwordInfoMessage, serviceName, accountName));
            consoleService.WriteInfo($"Password: {decryptedPassword}");
            consoleService.WriteInfo($"URL: {passwordEntry.Url}");
            consoleService.WriteInfo($"Category: {passwordEntry.Category}");

            if (passwordHealthResult.IsStrong && passwordHealthResult.IsUnique && !passwordHealthResult.IsCompromised)
            {
                consoleService.WriteInfo(passwordHealthyMessage);
            }
            else
            {
                consoleService.WriteWarning(warningMessage);
            }
        }
    }
}