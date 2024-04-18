using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Domain.Services.PasswordHealthCheck;

public class PasswordStrengthService : IPasswordStrengthService
{
    public bool IsStrongPassword(string password)
    {
        if (string.IsNullOrEmpty(password) || password.Length < 8)
        {
            return false; // too short or not provided
        }

        var hasUpperCaseLetter = false;
        var hasLowerCaseLetter = false;
        var hasDecimalDigit = false;
        var hasSpecialCharacter = false;

        foreach (char c in password)
        {
            if (char.IsUpper(c)) hasUpperCaseLetter = true;
            else if (char.IsLower(c)) hasLowerCaseLetter = true;
            else if (char.IsDigit(c)) hasDecimalDigit = true;
            else if (!char.IsLetterOrDigit(c)) hasSpecialCharacter = true;
        }
        
        var isStrong = hasUpperCaseLetter && hasLowerCaseLetter && hasDecimalDigit && hasSpecialCharacter;

        return isStrong;
    }
}