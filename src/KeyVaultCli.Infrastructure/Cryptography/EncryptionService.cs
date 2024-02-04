namespace KeyVaultCli.Infrastructure.Cryptography;

public class EncryptionService : IEncryptionService
{
    public string Encrypt(string password, string masterPassword)
    {
        // encryption logic here...
        return password;
    }

    public string Decrypt(string password, string masterPassword)
    {
        // decryption logic here...
        return password;
    }
}