using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Domain.Factories;

// Factory Pattern
public class VaultFactory(
    IConsoleService consoleService,
    IEncryptionService encryptionService,
    IFileService fileService, IPasswordGenerator passwordGenerator)
    : IVaultFactory
{
    public IVault? CreateVault(string masterPassword)
    {
        if(string.IsNullOrEmpty(masterPassword))
        {
            consoleService.WriteError("Master password should not be empty");
            return null;
        }

        var vault = new Vault(masterPassword, encryptionService, fileService, passwordGenerator);
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