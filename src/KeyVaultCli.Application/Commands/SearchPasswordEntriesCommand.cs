using KeyVaultCli.Domain;

namespace KeyVaultCli.Application.Commands;

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