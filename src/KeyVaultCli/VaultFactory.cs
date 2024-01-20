using KeyVaultCli.UI;

namespace KeyVaultCli;

// Factory Pattern
public static class VaultFactory
{
    public static Vault? CreateVault(string masterPassword)
    {
        if(string.IsNullOrEmpty(masterPassword))
        {
            ConsoleHelper.WriteError("Master password should not be empty");
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
            ConsoleHelper.WriteError("Invalid master password. Exit.");
            return null;
        }
        
        return vault;
    }
}