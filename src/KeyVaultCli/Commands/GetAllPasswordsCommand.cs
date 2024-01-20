using KeyVaultCli.UI;

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
            string[] headers = { "Service", "Account", "Password" };
    
            // Transform every PasswordEntry into a List of objects
            List<List<object>> dataRows = allPasswordEntries
                .Select(entry => new List<object>
                {
                    entry.ServiceName, 
                    entry.AccountName, 
                    _vault.GetPassword(entry.ServiceName, entry.AccountName)
                }).ToList();

            ConsoleHelper.WriteTable(headers, dataRows);
        }
        else
        {
            ConsoleHelper.WriteError("No password entries found.");
        }
    }
}