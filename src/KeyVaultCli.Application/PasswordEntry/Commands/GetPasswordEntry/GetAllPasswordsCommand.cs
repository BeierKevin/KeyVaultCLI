using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;
using KeyVaultCli.Domain.Services.PasswordHealthCheck;

namespace KeyVaultCli.Application.PasswordEntry.Commands.GetPasswordEntry;

public class GetAllPasswordsCommand : ICommand
{
    private readonly IVault vault;
    private readonly IConsole consoleService;
    private readonly PasswordHealthService passwordHealthService;
    
    public GetAllPasswordsCommand(IVault vault, IConsole consoleService)
    {
        this.vault = vault;
        this.consoleService = consoleService;
        this.passwordHealthService = new PasswordHealthService(
            new PasswordStrengthService(), new PasswordUniquenessService(this.vault), new CompromisedPasswordService());
    }
    
    public void Execute()
    {
        consoleService.WriteInfo("A healthy password should be unique within the vault, at least 8 characters long, and contain at least one uppercase letter, one lowercase letter, and one digit.");
        
        var allPasswordEntries = vault.LoadPasswordEntries();

        if (allPasswordEntries.Any())
        {
            string[] headers = { "GUID", "Service Name", "Account Name", "Password (Decrypted)", "URL", "Category", "Creation Date", "Last Modified Date", "Password Health" };

            // Transform every PasswordEntry into a List of objects
            var dataRows = allPasswordEntries
                .Select(entry => {
                    var decryptedPassword = vault.GetPassword(entry.ServiceName, entry.AccountName);
                    var passwordHealthResult = passwordHealthService.CheckPasswordHealthAsync(decryptedPassword).Result;

                    var passwordHealthDescription = passwordHealthResult.IsStrong && passwordHealthResult.IsUnique && !passwordHealthResult.IsCompromised
                        ? "Healthy"
                        : "Not Healthy";

                    return new List<object>
                    {
                        entry.EntryId,
                        entry.ServiceName, 
                        entry.AccountName, 
                        decryptedPassword,
                        entry.Url,
                        entry.Category,
                        entry.CreationDate,
                        entry.LastModifiedDate,
                        passwordHealthDescription
                    };
                }).ToList();

            consoleService.WriteTable(headers, dataRows);
        }
        else
        {
            consoleService.WriteError("No password entries found.");
        }
    }
}