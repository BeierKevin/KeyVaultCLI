using KeyVaultCli.Core;

namespace KeyVaultCli.Commands;

public class SearchPasswordEntriesCommand : ICommand
{
    private readonly Vault _vault;

    public SearchPasswordEntriesCommand(Vault vault)
    {
        _vault = vault;
    }

    public void Execute()
    {
        Console.Write("Enter your search query: ");
        var query = Console.ReadLine();

        var matchingEntries = _vault.SearchPasswordEntries(query);

        if (matchingEntries.Any())
        {
            Console.WriteLine("Matching entries:");
            foreach (var entry in matchingEntries)
            {
                Console.WriteLine($"Service: {entry.ServiceName}, Account: {entry.AccountName}");
            }
        }
        else
        {
            Console.WriteLine("No matching entries found.");
        }
    }
}