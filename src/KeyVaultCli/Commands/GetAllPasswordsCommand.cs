using KeyVaultCli.Core;

namespace KeyVaultCli.Commands;

public class GetAllPasswordsCommand : ICommand
{
    private readonly Vault _vault;

    public GetAllPasswordsCommand(Vault vault)
    {
        _vault = vault;
    }

    public void Execute()
    {
        var allPasswordEntries = _vault.LoadPasswordEntries();

        if (allPasswordEntries != null && allPasswordEntries.Any())
        {
            Console.WriteLine("All password entries: ");
            foreach (var entry in allPasswordEntries)
            {
                Console.WriteLine($"Service: {entry.ServiceName}, Account: {entry.AccountName}, Password: {_vault
                    .GetPassword(entry.ServiceName, entry.AccountName)}");
                // if your password entries have additional fields, print them here too
            }
        }
        else
        {
            Console.WriteLine("No password entries found.");
        }
    }
}