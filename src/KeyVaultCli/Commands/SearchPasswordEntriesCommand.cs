using CLI.UI;

namespace CLI.Commands;

public class SearchPasswordEntriesCommand : ICommand
{
    private readonly Vault _vault;

    public SearchPasswordEntriesCommand(Vault vault)
    {
        _vault = vault;
    }

    public void Execute()
    {
        var query = ConsoleHelper.GetInput("Enter your search query: ");

        var matchingEntries = _vault.SearchPasswordEntries(query);

        if (matchingEntries.Any())
        {
            ConsoleHelper.WriteInfo("Matching entries:");
            foreach (var entry in matchingEntries)
            {
                ConsoleHelper.WriteInfo($"Service: {entry.ServiceName}, Account: {entry.AccountName}");
            }
        }
        else
        {
            ConsoleHelper.WriteError("No matching entries found.");
        }
    }
}