namespace KeyVaultCli.Domain.Common.Interfaces;

public interface IPasswordStrengthService
{
    bool IsStrongPassword(string password);
}