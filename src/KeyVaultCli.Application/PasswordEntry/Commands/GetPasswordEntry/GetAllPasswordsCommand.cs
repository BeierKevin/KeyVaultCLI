using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Application.PasswordEntry.Commands.GetPasswordEntry;

public class GetAllPasswordsCommand(IVault vault, IConsole consoleService) : ICommand
{
    public void Execute()
    {
        var allPasswordEntries = vault.LoadPasswordEntries();

        if (allPasswordEntries.Any())
        {
            string[] headers = { "GUID", "Service Name", "AccountName", "Password (Decrypted)", "Creation Date", "Last Modified Date", };

            // Transform every PasswordEntry into a List of objects
            List<List<object>> dataRows = allPasswordEntries
                .Select(entry => new List<object>
                {
                    entry.EntryId,
                    entry.ServiceName, 
                    entry.AccountName, 
                    vault.GetPassword(entry.ServiceName, entry.AccountName),
                    entry.CreationDate,
                    entry.LastModifiedDate
                }).ToList();

            consoleService.WriteTable(headers, dataRows);
        }
        else
        {
            consoleService.WriteError("No password entries found.");
        }
    }
}