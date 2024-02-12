using System.Security.Cryptography;
using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Infrastructure.Cryptography;

public class PasswordGenerator : IPasswordGenerator
{
    private static readonly char[] AllowableCharacters = Enumerable
        .Range(0, 26)
        .SelectMany(i => new [] 
        { 
            (char)('a' + i),  // generate a-z
            (char)('A' + i),  // generate A-Z
        })
        .Concat(Enumerable.Range('0', 10).Select(i => (char)i))  // generate 0-9
        .ToArray();
    
    [Obsolete("Obsolete")]
    public string GeneratePassword(int length)
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
            var characterIndex = (int)(AllowableCharacters.Length * (value / (1.0 + ulong.MaxValue)));
            result[i] = AllowableCharacters[characterIndex];
        }

        return new string(result);
    }
}