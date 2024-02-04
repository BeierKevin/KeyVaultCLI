namespace KeyVaultCli.Domain.Commands;

public class SearchPasswordEntriesCommand : ICommand
{
    private readonly IVault _vault;
    private readonly IConsole _consoleService;

    public SearchPasswordEntriesCommand(IVault vault, IConsole consoleService)
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