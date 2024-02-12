using KeyVaultCli.Domain;

namespace KeyVaultCli.Application.PasswordEntry.Common.Interfaces;

public interface IVaultFactory
{
    IVault? CreateVault(string masterPassword);
}