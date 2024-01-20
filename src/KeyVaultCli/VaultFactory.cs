namespace KeyVaultCli.Core;

public static class VaultFactory
{
    public static Vault? CreateVault(string masterPassword)
    {
        if(string.IsNullOrEmpty(masterPassword))
        {
            Console.WriteLine("Master password should not be empty");
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
            Console.WriteLine("Invalid master password. Exit.");
            return null;
        }
        
        return vault;
    }
}