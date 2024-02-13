namespace KeyVaultCli.Domain.Common.Interfaces;

public interface IVaultEncryptionService
{
    string Encrypt(string password, string masterPassword);
    string Decrypt(string password, string masterPassword);
}