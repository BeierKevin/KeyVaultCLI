namespace KeyVaultCli.Domain.Common.Interfaces;

public interface IVaultFactory
{
    IVault? CreateVault(string masterPassword);
    bool DeleteVault();
    IVault GetVault();
}