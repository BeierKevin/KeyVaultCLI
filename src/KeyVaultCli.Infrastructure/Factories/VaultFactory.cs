using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain;
using KeyVaultCli.Domain.Entities;
using KeyVaultCli.Infrastructure.Cryptography;

namespace KeyVaultCli.Infrastructure.Factories;

// Factory Pattern
public class VaultFactory(
    IConsoleService consoleService,
    IEncryptionService encryptionService,
    IFileService fileService)
    : IVaultFactory
{
    public IVault? CreateVault(string masterPassword)
    {
        if(string.IsNullOrEmpty(masterPassword))
        {
            consoleService.WriteError("Master password should not be empty");
            return null;
        }

        var vault = new Vault(masterPassword, encryptionService, fileService);
        var savedPassword = vault.LoadMasterPassword();
        if(savedPassword == null)
        {
            vault.SaveMasterPassword();
        }
        else if(savedPassword != masterPassword)
        {
            consoleService.WriteError("Invalid master password. Exit.");
            return null;
        }
        
        return vault;
    }
}