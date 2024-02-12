namespace KeyVaultCli.Domain.Common.Interfaces;

public interface IPasswordGenerator
{
    string GeneratePassword(int length);
}