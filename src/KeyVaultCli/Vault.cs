using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using KeyVaultCli.Crypto;
using KeyVaultCli.Security.Passwords;
using KeyVaultCli.Security.IO;

namespace KeyVaultCli.Core;

// Could be a Singelton ?
public class Vault
{ 
    private readonly List<PasswordEntry> passwordEntries;
    // Factory Pattern: Since the Vault needs a master password to initialize, you may have a factory that verifies the master password before returning a new Vault instance.
    private string masterPassword;
    // Saved files will be in the /bin/Debug/netX.X folder
    private readonly string filePath = "vault.dat";
    private string passwordFile = "masterpassword.dat";

    public Vault(string masterPassword)
    {
        this.masterPassword = masterPassword;
        this.passwordEntries = LoadPasswordEntries();
    }
    
    public void SaveMasterPassword()
    {
        var encryptedPassword = EncryptionHelper.Encrypt(this.masterPassword, masterPassword);
        File.WriteAllText(passwordFile, encryptedPassword);
    }

    public string LoadMasterPassword()
    {
        if (File.Exists(passwordFile))
        {
            var encryptedPassword = File.ReadAllText(passwordFile);
            return EncryptionHelper.Decrypt(encryptedPassword, masterPassword);
        }
        return null;
    }
    
    public bool UpdateMasterPassword(string oldPassword, string newPassword)
    {
        // Verify the old password matches the current master password
        if (!String.Equals(oldPassword, masterPassword)) // replace with your verification logic if different
        {
            return false;
        }
    
        // Now update the master password
        masterPassword = newPassword;  // replace with your updating code if different
        return true;
    }
    
    public void AddPasswordEntry(string serviceName, string accountName, string password)
    {
        var encryptedPassword = EncryptionHelper.Encrypt(password, masterPassword);
    
        var entry = new PasswordEntryBuilder()
            .SetServiceName(serviceName)
            .SetAccountName(accountName)
            .SetEncryptedPassword(encryptedPassword)
            .Build();

        passwordEntries.Add(entry);
        SavePasswordEntries();
    }

    private void SavePasswordEntries()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(passwordEntries, options);
        File.WriteAllText(filePath, json);
    }
    
    public bool DeletePasswordEntry(string serviceName, string accountName)
    {
        passwordEntries.RemoveAll(x => 
            string.Equals(x.ServiceName, serviceName, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(x.AccountName, accountName, StringComparison.OrdinalIgnoreCase));
        SavePasswordEntries();
        
        // Check if password entry exists
        var entryExists = passwordEntries.Any(x => 
            string.Equals(x.ServiceName, serviceName, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(x.AccountName, accountName, StringComparison.OrdinalIgnoreCase));

        if (entryExists)
        {
            passwordEntries.RemoveAll(x => 
                string.Equals(x.ServiceName, serviceName, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(x.AccountName, accountName, StringComparison.OrdinalIgnoreCase));
        
            SavePasswordEntries();
        }

        // Return true if an entry was removed, otherwise return false
        return entryExists;
    }
    
    public void DeleteAllPasswordEntries()
    {
        passwordEntries.Clear();
        SavePasswordEntries();
    }

    public List<PasswordEntry> LoadPasswordEntries()
    {
        if (!File.Exists(filePath))
        {
            return new List<PasswordEntry>();
        }
    
        var json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<PasswordEntry>>(json);
    }

    public string? GetPassword(string? serviceName, string? accountName)
    {
        if (serviceName is null || accountName is null) return null;

        var passwordEntry = passwordEntries.FirstOrDefault(entry => 
            entry.ServiceName == serviceName && entry.AccountName == accountName);

        return passwordEntry != null
            ? EncryptionHelper.Decrypt(passwordEntry.EncryptedPassword, masterPassword)
            : null;
    }
    
    public bool UpdatePasswordEntry(string currentServiceName, string currentAccountName, string newServiceName, string newAccountName, int passwordLength, string newPassword = null)
    {
        var passwordEntry = passwordEntries.FirstOrDefault(x =>
            x.ServiceName == currentServiceName && x.AccountName == currentAccountName);

        if (passwordEntry == null)
            return false;

        passwordEntry.ServiceName = newServiceName;
        passwordEntry.AccountName = newAccountName;

        // If newPassword is null, generate a random password
        passwordEntry.EncryptedPassword = EncryptionHelper.Encrypt(newPassword ?? PasswordGenerator.GeneratePassword(passwordLength), masterPassword);

        SavePasswordEntries();
        return true;
    }
    
    public List<PasswordEntry> SearchPasswordEntries(string searchTerm)
    {
        return passwordEntries.Where(x => 
            x.ServiceName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) 
            || x.AccountName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
        ).ToList();
    }
    
    public string GenerateAndAddPasswordEntry(string serviceName, string accountName, int passwordLength)
    {
        var password = PasswordGenerator.GeneratePassword(passwordLength);  // Generate a password
        AddPasswordEntry(serviceName, accountName, password);  // Add it to password entries
        return password;
    }
}