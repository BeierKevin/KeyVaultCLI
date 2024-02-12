using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain;
using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Application.PasswordEntry.Commands.GetPasswordEntry;

public class SearchPasswordEntriesCommand(IVault vault, IConsole consoleService) : ICommand
{
    public void Execute()
    {
        var query = consoleService.GetInput("Enter your search query: ");

        var matchingEntries = vault.SearchPasswordEntries(query);

        if (matchingEntries.Any())
        {
            consoleService.WriteInfo("Matching entries:");
            foreach (var entry in matchingEntries)
            {
                consoleService.WriteInfo($"Service: {entry.ServiceName}, Account: {entry.AccountName}");
            }
        }
        else
        {
            consoleService.WriteError("No matching entries found.");
        }
    }
}