using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KeyVaultCli.Application.PasswordEntry.Commands.GetPasswordEntry
{
    public class SearchPasswordEntriesCommand(IVault vault, IConsoleService consoleService) : ICommand
    {
        private readonly IVault vault = vault ?? throw new ArgumentNullException(nameof(vault));

        private readonly IConsoleService consoleService =
            consoleService ?? throw new ArgumentNullException(nameof(consoleService));

        private readonly string searchPrompt = "Enter your search query: ";
        private readonly string matchingEntriesMessage = "Matching search entries:";
        private readonly string noEntriesFoundError = "No matching entries found.";

        private readonly string[] headers =
        {
            "GUID", "Service Name", "AccountName", "Password (Decrypted)", "URL", "Category", "Creation Date",
            "Last Modified Date",
        };

        public void Execute()
        {
            try
            {
                var query = consoleService.GetInputFromPrompt(searchPrompt);

                var matchingEntries = vault.SearchPasswordEntries(query);

                if (matchingEntries != null && matchingEntries.Any())
                {
                    consoleService.WriteInfo(matchingEntriesMessage);
                    var dataRows = GetMatchingEntriesDataRows(matchingEntries);
                    consoleService.WriteTable(headers, dataRows);
                }
                else
                {
                    consoleService.WriteError(noEntriesFoundError);
                }
            }
            catch (Exception ex)
            {
                consoleService.WriteError($"An error occurred: {ex.Message}");
            }
        }

        private List<List<object>> GetMatchingEntriesDataRows(
            IEnumerable<Domain.Entities.PasswordEntry> matchingEntries)
        {
            return matchingEntries
                .Where(entry => entry != null)
                .Select(entry =>
                {
                    var password = vault.GetPassword(entry.ServiceName, entry.AccountName);
                    return new List<object>
                    {
                        entry.EntryId,
                        entry.ServiceName,
                        entry.AccountName,
                        password != null ? password : "N/A",
                        entry.Url,
                        entry.Category,
                        entry.CreationDate,
                        entry.LastModifiedDate
                    };
                }).ToList();
        }
    }
}