using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Domain.Services.PasswordHealthCheck;

public class CompromisedPasswordService : ICompromisedPasswordService
{
    public async Task<bool> HasBeenCompromisedAsync(string password)
    {
        // Implement your logic here
        return false;
    }
}