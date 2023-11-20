using System;
using System.Security.Cryptography;
using System.Text;

namespace KeyVaultCli.Security.Passwords;

public class PasswordGenerator
{
    private const string NumericChars = "0123456789";
    private const string LetterChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
    private const string SpecialChars = "!@#$%^&*()-=_+[]{}|;:'\",.<>/?";

    public static string GeneratePassword(int length, bool includeNumeric, bool includeLetters,
        bool includeSpecialSymbols)
    {
        if (length <= 0)
        {
            throw new ArgumentException("Password length must be greater than 0.", nameof(length));
        }

        if (!includeNumeric && !includeLetters && !includeSpecialSymbols)
        {
            throw new ArgumentException(
                "At least one character set (numeric, letters, special symbols) must be included.",
                nameof(includeNumeric));
        }

        StringBuilder password = new StringBuilder();
        string charSet = GetCharSet(includeNumeric, includeLetters, includeSpecialSymbols);

        using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
        {
            byte[] randomBytes = new byte[length];
            rng.GetBytes(randomBytes);

            foreach (byte b in randomBytes)
            {
                password.Append(charSet[b % charSet.Length]);
            }
        }

        return password.ToString();
    }

    private static string GetCharSet(bool includeNumeric, bool includeLetters, bool includeSpecialSymbols)
    {
        StringBuilder charSet = new StringBuilder();

        if (includeNumeric)
        {
            charSet.Append(NumericChars);
        }

        if (includeLetters)
        {
            charSet.Append(LetterChars);
        }

        if (includeSpecialSymbols)
        {
            charSet.Append(SpecialChars);
        }

        return charSet.ToString();
    }
}