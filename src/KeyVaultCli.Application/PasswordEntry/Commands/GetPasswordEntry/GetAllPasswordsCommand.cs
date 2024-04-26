using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;
using KeyVaultCli.Domain.Services.PasswordHealthCheck;
using System;
using System.Linq;
using System.Collections.Generic;

namespace KeyVaultCli.Application.PasswordEntry.Commands.GetPasswordEntry
{
    public class GetAllPasswordsCommand : ICommand
    {
        private readonly IVault vault;
        private readonly IConsoleService consoleService;
        private readonly PasswordHealthService passwordHealthService;

        private const string infoMessage =
            "A healthy password should be unique within the vault, at least 8 characters long, and contain at least one uppercase letter, one lowercase letter, and one digit.";

        private static readonly string[] headers =
        {
            "GUID", "Service Name", "Account Name", "Password (Decrypted)", "URL", "Category", "Creation Date",
            "Last Modified Date", "Password Health"
        };

        private const string errorMessage = "No password entries found.";

        public GetAllPasswordsCommand(IVault vault, IConsoleService consoleService)
        {
            this.vault = vault ?? throw new ArgumentNullException(nameof(vault));
            this.consoleService = consoleService ?? throw new ArgumentNullException(nameof(consoleService));
            this.passwordHealthService = new PasswordHealthService(
                new PasswordStrengthService(), new PasswordUniquenessService(this.vault),
                new CompromisedPasswordService());
        }

        public void Execute()
        {
            try
            {
                consoleService.WriteInfo(infoMessage);

                var allPasswordEntries = vault.LoadPasswordEntries();

                if (allPasswordEntries.Any())
                {
                    // Transform every PasswordEntry into a List of objects
                    var dataRows = allPasswordEntries
                        .Select(entry =>
                        {
                            var decryptedPassword = vault.GetPassword(entry.ServiceName, entry.AccountName);
                            var passwordHealthResult =
                                passwordHealthService.CheckPasswordHealthAsync(decryptedPassword).Result;

                            var passwordHealthDescription = GetPasswordHealthDescription(passwordHealthResult);

                            return new List<object>
                            {
                                entry.EntryId,
                                entry.ServiceName,
                                entry.AccountName,
                                decryptedPassword,
                                entry.Url,
                                entry.Category,
                                entry.CreationDate,
                                entry.LastModifiedDate,
                                passwordHealthDescription
                            };
                        }).ToList();

                    consoleService.WriteTable(headers, dataRows);
                }
                else
                {
                    consoleService.WriteError(errorMessage);
                }
            }
            catch (Exception ex)
            {
                consoleService.WriteError("An error occurred while trying to get all passwords. Details: " +
                                          ex.Message);
            }
        }

        private static string GetPasswordHealthDescription(PasswordHealthResult passwordHealthResult)
        {
            return passwordHealthResult.IsStrong && passwordHealthResult.IsUnique && !passwordHealthResult.IsCompromised
                ? "Healthy"
                : "Not Healthy";
        }
    }
}