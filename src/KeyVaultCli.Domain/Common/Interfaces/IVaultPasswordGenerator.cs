namespace KeyVaultCli.Domain.Common.Interfaces;

public interface IVaultPasswordGenerator
{
    string GeneratePassword(int length);
}