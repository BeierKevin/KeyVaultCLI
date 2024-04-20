using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Application.PasswordEntry.Commands.GetPasswordEntry;

public class SearchPasswordEntriesCommand(IVault vault, IConsole consoleService) : ICommand
{
    private readonly string searchPrompt = "Enter your search query: ";
    private readonly string matchingEntriesMessage = "Matching search entries:";
    private readonly string noEntriesFoundError = "No matching entries found.";
    private readonly string[] headers = { "GUID", "Service Name", "AccountName", "Password (Decrypted)", "URL", "Category", "Creation Date", "Last Modified Date", };
    
    public void Execute()
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

    private List<List<object>> GetMatchingEntriesDataRows(IEnumerable<Domain.Entities.PasswordEntry> matchingEntries)
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