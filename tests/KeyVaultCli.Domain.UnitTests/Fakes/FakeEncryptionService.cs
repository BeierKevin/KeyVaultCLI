using KeyVaultCli.Application.Common.Interfaces;

namespace KeyVaultCli.Domain.UnitTests.Fakes;

public class FakeEncryptionService : IEncryptionService
{
    private const string FakeEncryptionSuffix = "_encrypted";

    public string Encrypt(string password, string masterPassword)
    {
        return password + FakeEncryptionSuffix;
    }

    public string Decrypt(string encryptedPassword, string masterPassword)
    {
        if (encryptedPassword.EndsWith(FakeEncryptionSuffix))
        {
            return encryptedPassword.Substring(0, encryptedPassword.Length - FakeEncryptionSuffix.Length);
        }

        return encryptedPassword;
    }
}