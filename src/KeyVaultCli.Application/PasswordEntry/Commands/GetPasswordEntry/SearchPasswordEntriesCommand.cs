using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Application.PasswordEntry.Commands.GetPasswordEntry;

public class SearchPasswordEntriesCommand(IVault vault, IConsole consoleService) : ICommand
{
    public void Execute()
    {
        var query = consoleService.GetInputFromPrompt("Enter your search query: ");

        var matchingEntries = vault.SearchPasswordEntries(query);

        if (matchingEntries != null && matchingEntries.Any())
        {
            consoleService.WriteInfo("Matching search entries:");
    
            string[] headers = { "GUID", "Service Name", "AccountName", "Password (Decrypted)", "URL", "Category", "Creation Date", "Last Modified Date", };

            // Transform every PasswordEntry into a List of objects
            var dataRows = matchingEntries
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

            consoleService.WriteTable(headers, dataRows);
        }
        else
        {
            consoleService.WriteError("No matching entries found.");
        }
    }
}