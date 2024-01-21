using KeyVaultCli.Core;

namespace KeyVaultCli.Domain;

public interface IVaultFactory
{
    Vault? CreateVault(string masterPassword);
}