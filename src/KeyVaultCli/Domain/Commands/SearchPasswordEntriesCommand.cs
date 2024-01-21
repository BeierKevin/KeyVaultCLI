using KeyVaultCli.Application;
using KeyVaultCli.Domain.Entities;

namespace KeyVaultCli.Domain.Commands;

public class SearchPasswordEntriesCommand : ICommand
{
    private readonly Vault _vault;
    private readonly IConsoleService _consoleService;

    public SearchPasswordEntriesCommand(Vault vault, IConsoleService consoleService)
    {
        _vault = vault;
        _consoleService = consoleService;
    }

    public void Execute()
    {
        var query = _consoleService.GetInput("Enter your search query: ");

        var matchingEntries = _vault.SearchPasswordEntries(query);

        if (matchingEntries.Any())
        {
            _consoleService.WriteInfo("Matching entries:");
            foreach (var entry in matchingEntries)
            {
                _consoleService.WriteInfo($"Service: {entry.ServiceName}, Account: {entry.AccountName}");
            }
        }
        else
        {
            _consoleService.WriteError("No matching entries found.");
        }
    }
}