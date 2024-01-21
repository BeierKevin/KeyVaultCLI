using KeyVaultCli.Domain;
using KeyVaultCli.Domain.Entities;

namespace KeyVaultCli.Application;

public interface IVaultFactory
{
    Vault? CreateVault(string masterPassword);
}