using KeyVaultCli.Application;
using KeyVaultCli.Domain;
using KeyVaultCli.Infrastructure.UI;

namespace KeyVaultCli.Core;

// Factory Pattern
public class VaultFactory : IVaultFactory
{
    private readonly IConsoleService _consoleService;
    
    public VaultFactory(IConsoleService consoleService)
    {
        _consoleService = consoleService;
    }

    public Vault? CreateVault(string masterPassword)
    {
        if(string.IsNullOrEmpty(masterPassword))
        {
            _consoleService.WriteError("Master password should not be empty");
            return null;
        }

        var vault = new Vault(masterPassword);
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