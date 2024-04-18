using KeyVaultCli.Application.Common.Interfaces;

namespace KeyVaultCli.Infrastructure.Services;

public class PasswordGeneratorService : IPasswordGeneratorService
{
    const string lowerCase = "abcdefghijklmnopqrstuvwxyz";
    const string upperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    const string digits = "1234567890";
    const string specialCharacters = "!@#$%^&*_-+=:;<>?/";

    public string GeneratePassword(int length)
    {
        var allowed = lowerCase + upperCase + digits + specialCharacters;

        // Using a cryptographically strong random number generator.
        var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
        var byteBuffer = new byte[length];

        rng.GetBytes(byteBuffer);

        var characters = new char[length];

        // Ensuring the password meets the complexity requirement.
        var position = 0;
        characters[position++] = upperCase[GetRandomNumber(rng, upperCase.Length)];
        characters[position++] = lowerCase[GetRandomNumber(rng, lowerCase.Length)];
        characters[position++] = digits[GetRandomNumber(rng, digits.Length)];
        characters[position++] = specialCharacters[GetRandomNumber(rng, specialCharacters.Length)];

        for (int iter = position; iter < length; iter++)
        {
            characters[iter] = allowed[GetRandomNumber(rng, allowed.Length)];
        }

        // Shuffling the characters.
        return new string(characters.OrderBy(c => GetRandomNumber(rng, length)).ToArray());
    }

    private static int GetRandomNumber(System.Security.Cryptography.RNGCryptoServiceProvider rng, int max)
    {
        var randomBytes = new byte[4]; // 4 for int32
        rng.GetBytes(randomBytes);
        var randomInteger = BitConverter.ToInt32(randomBytes, 0);
        return Math.Abs(randomInteger % max);
    }
}