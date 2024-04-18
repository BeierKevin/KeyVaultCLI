namespace KeyVaultCli.Domain.Common.Interfaces;

public interface IPasswordUniquenessService
{
    Task<bool> IsUniquePasswordAsync(string password);
}