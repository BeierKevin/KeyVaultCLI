using System.Security.Cryptography;

namespace KeyVaultCli.Cryptography;

public static class PasswordGenerator
{
    private static readonly char[] allowableCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

    public static string GeneratePassword(int length)
    {
        var bytes = new byte[length * 8];
        using (var cryptoProvider = new RNGCryptoServiceProvider())
        {
            cryptoProvider.GetBytes(bytes);
        }
        
        var result = new char[length];
        for (var i = 0; i < length; ++i)
        {
            var value = BitConverter.ToUInt64(bytes, i * 8);
            var characterIndex = (int)(allowableCharacters.Length * (value / (1.0 + ulong.MaxValue)));
            result[i] = allowableCharacters[characterIndex];
        }

        return new string(result);
    }
}