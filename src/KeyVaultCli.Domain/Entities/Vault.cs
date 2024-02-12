using System.Text.Json;
using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Domain.Entities;

public class Vault : IVault
{ 
    private readonly List<PasswordEntry> _passwordEntries;
    // Factory Pattern: Since the Vault needs a master password to initialize, you may have a factory that verifies the master password before returning a new Vault instance.
    private string _masterPassword;
    // Saved files will be in the /bin/Debug/netX.X folder
    private readonly string _filePath = "vault.dat";
    private readonly string _passwordFile = "masterpassword.dat";
    private readonly IEncryptionService _encryptionService;
    private readonly IFileService _fileService;
    private readonly IPasswordGenerator _passwordGenerator;

    public Vault(string masterPassword, IEncryptionService encryptionService, IFileService fileService, IPasswordGenerator passwordGenerator)
    {
        _masterPassword = masterPassword;
        _encryptionService = encryptionService;
        _fileService = fileService;
        _passwordGenerator = passwordGenerator;
        _passwordEntries = LoadPasswordEntries();
    }
    
    public void SaveMasterPassword()
    {
        var encryptedPassword = _encryptionService.Encrypt(this._masterPassword, _masterPassword);
        _fileService.WriteAllText(_passwordFile, encryptedPassword);
    }

    public string LoadMasterPassword()
    {
        if (_fileService.Exists(_passwordFile))
        {
            var encryptedPassword = _fileService.ReadAllText(_passwordFile);
            return _encryptionService.Decrypt(encryptedPassword, _masterPassword);
        }
        return null;
    }
    
    public bool UpdateMasterPassword(string oldPassword, string newPassword)
    {
        if (!string.Equals(oldPassword, _masterPassword))
        {
            return false;
        }
    
        // Now update the master password
        _masterPassword = newPassword;
        SaveMasterPassword();
        return true;
    }
    
    public void AddPasswordEntry(string serviceName, string accountName, string password)
    {
        var encryptedPassword = _encryptionService.Encrypt(password, _masterPassword);
    
        var entry = new PasswordEntryBuilder()
            .SetServiceName(serviceName)
            .SetAccountName(accountName)
            .SetEncryptedPassword(encryptedPassword)
            .Build();

        _passwordEntries.Add(entry);
        SavePasswordEntries();
    }

    public void SavePasswordEntries()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(_passwordEntries, options);
        _fileService.WriteAllText(_filePath, json);
    }
    
    public bool DeletePasswordEntry(string serviceName, string accountName)
    {
        var entryExists = _passwordEntries.Any(x => 
            string.Equals(x.ServiceName, serviceName, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(x.AccountName, accountName, StringComparison.OrdinalIgnoreCase));

        if (entryExists)
        {
            _passwordEntries.RemoveAll(x => 
                string.Equals(x.ServiceName, serviceName, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(x.AccountName, accountName, StringComparison.OrdinalIgnoreCase));
        
            SavePasswordEntries();
            return true;
        }

        return false;
    }
    
    public void DeleteAllPasswordEntries()
    {
        _passwordEntries.Clear();
        SavePasswordEntries();
    }

    public List<PasswordEntry> LoadPasswordEntries()
    {
        if (!_fileService.Exists(_filePath))
        {
            return new List<PasswordEntry>();
        }
    
        var json = _fileService.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<PasswordEntry>>(json);
    }

    public string? GetPassword(string? serviceName, string? accountName)
    {
        if (serviceName is null || accountName is null) return null;

        var passwordEntry = _passwordEntries.FirstOrDefault(entry => 
            entry.ServiceName == serviceName && entry.AccountName == accountName);

        return passwordEntry != null
            ? _encryptionService.Decrypt(passwordEntry.EncryptedPassword, _masterPassword)
            : null;
    }
    
    public bool UpdatePasswordEntry(string currentServiceName, string currentAccountName, string newServiceName, string newAccountName, int passwordLength, string newPassword = null)
    {
        var passwordEntry = _passwordEntries.FirstOrDefault(x =>
            x.ServiceName == currentServiceName && x.AccountName == currentAccountName);

        if (passwordEntry == null)
            return false;

        passwordEntry.ServiceName = newServiceName;
        passwordEntry.AccountName = newAccountName;

        // If newPassword is null, generate a random password
        passwordEntry.EncryptedPassword = _encryptionService.Encrypt(newPassword ?? _passwordGenerator.GeneratePassword(passwordLength), _masterPassword);

        SavePasswordEntries();
        return true;
    }
    
    public List<PasswordEntry> SearchPasswordEntries(string searchTerm)
    {
        return _passwordEntries.Where(x => 
            x.ServiceName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) 
            || x.AccountName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
        ).ToList();
    }
    
    public string GenerateAndAddPasswordEntry(string serviceName, string accountName, int passwordLength)
    {
        var password = _passwordGenerator.GeneratePassword(passwordLength);  // Generate a password
        AddPasswordEntry(serviceName, accountName, password);  // Add it to password entries
        return password;
    }
}