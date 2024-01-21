using KeyVaultCli.Application;
using KeyVaultCli.Domain.Entities;
using KeyVaultCli.Infrastructure.Cryptography;

namespace KeyVaultCli.Infrastructure;

// Factory Pattern
public class VaultFactory : IVaultFactory
{
    private readonly IConsoleService _consoleService;
    private readonly IEncryptionService _encryptionService;
    private readonly IFileService _fileService;
    
    public VaultFactory(IConsoleService consoleService, IEncryptionService encryptionService, IFileService fileService)
    {
        _consoleService = consoleService;
        _encryptionService = encryptionService;
        _fileService = fileService;
    }

    public Vault? CreateVault(string masterPassword)
    {
        if(string.IsNullOrEmpty(masterPassword))
        {
            _consoleService.WriteError("Master password should not be empty");
            return null;
        }

        var vault = new Vault(masterPassword, _encryptionService, _fileService);
        var savedPassword = vault.LoadMasterPassword();
        if(savedPassword == null)
        {
            vault.SaveMasterPassword();
        }
        else if(savedPassword != masterPassword)
        {
            _consoleService.WriteError("Invalid master password. Exit.");
            return null;
        }
        
        return vault;
    }
}