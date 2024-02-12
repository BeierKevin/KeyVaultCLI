namespace KeyVaultCli.Domain.Common.Interfaces;

public interface IEncryptionService
{
    string Encrypt(string password, string masterPassword);
    string Decrypt(string password, string masterPassword);
}