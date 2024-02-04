using KeyVaultCli.Domain;
using KeyVaultCli.Domain.Entities;

namespace KeyVaultCli.Application;

public interface IVaultFactory
{
    IVault? CreateVault(string masterPassword);
}