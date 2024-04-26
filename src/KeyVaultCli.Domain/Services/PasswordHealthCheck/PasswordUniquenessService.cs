using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Domain.Services.PasswordHealthCheck;

public class PasswordUniquenessService : IPasswordUniquenessService
{
    private readonly IVault _vault;

    public PasswordUniquenessService(IVault vault)
    {
        _vault = vault;
    }

    public async Task<bool> IsUniquePasswordAsync(string password)
    {
        // TODO: proper implementation
        // var allDecryptedPasswords = _vault.GetAllDecryptedPasswords();
        //
        // foreach (var entry in allDecryptedPasswords)
        // {
        //     var decryptedPassword = entry.Value;
        //
        //     if (decryptedPassword == password)
        //     {
        //         return false;
        //     }
        // }

        return true;
    }
}