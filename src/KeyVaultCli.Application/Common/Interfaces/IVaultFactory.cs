using KeyVaultCli.Domain;

namespace KeyVaultCli.Application.Common.Interfaces;

public interface IVaultFactory
{
    IVault? CreateVault(string masterPassword);
}