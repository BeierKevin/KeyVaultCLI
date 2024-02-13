using System.Text.Json;
using KeyVaultCli.Domain.Common.Interfaces;
using KeyVaultCli.Domain.Exceptions;
using KeyVaultCli.Domain.Factories;

namespace KeyVaultCli.Domain.Entities;

public class Vault : IVault
{ 
    private readonly List<PasswordEntry> _passwordEntries;
    // Factory Pattern: Since the Vault needs a master password to initialize, you may have a factory that verifies the master password before returning a new Vault instance.
    private string _masterPassword;
    
    // Saved files will be in the /bin/Debug/netX.X folder
    private readonly string _passwordEntryFilePath;
    private readonly string _masterPasswordFilePath;
    private readonly IVaultEncryptionService _vaultEncryptionService;
    private readonly IVaultFileService _vaultFileService;
    private readonly IVaultPasswordGenerator _vaultPasswordGenerator;

    public Vault(string vaultFilePath, string masterPasswordFilePath, string masterPassword, IVaultEncryptionService 
            vaultEncryptionService, 
        IVaultFileService 
            vaultFileService, IVaultPasswordGenerator vaultPasswordGenerator)
    {
        _passwordEntryFilePath = vaultFilePath;
        _masterPasswordFilePath = masterPasswordFilePath;
        _masterPassword = masterPassword;
        _vaultEncryptionService = vaultEncryptionService;
        _vaultFileService = vaultFileService;
        _vaultPasswordGenerator = vaultPasswordGenerator;
        _passwordEntries = LoadPasswordEntries();
    }
    
    public void SaveMasterPassword()
    {
        var encryptedPassword = _vaultEncryptionService.Encrypt(this._masterPassword, _masterPassword);
        _vaultFileService.WriteAllText(_masterPasswordFilePath, encryptedPassword);
    }

    public string? LoadMasterPassword()
    {
        if (!_vaultFileService.Exists(_masterPasswordFilePath)) return null;
        var encryptedPassword = _vaultFileService.ReadAllText(_masterPasswordFilePath);
        return _vaultEncryptionService.Decrypt(encryptedPassword, _masterPassword);
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
        var encryptedPassword = _vaultEncryptionService.Encrypt(password, _masterPassword);
    
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
        _vaultFileService.WriteAllText(_passwordEntryFilePath, json);
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
        if (!_vaultFileService.Exists(_passwordEntryFilePath))
        {
            return new List<PasswordEntry>();
        }

        var json = _vaultFileService.ReadAllText(_passwordEntryFilePath);
        return JsonSerializer.Deserialize<List<PasswordEntry>>(json) ?? new List<PasswordEntry>();
    }

    public string GetPassword(string serviceName, string accountName)
    {
        if (serviceName is null || accountName is null)
            throw new ArgumentNullException(serviceName is null ? nameof(serviceName) : nameof(accountName));

        var passwordEntry = _passwordEntries.FirstOrDefault(entry =>
            entry.ServiceName == serviceName && entry.AccountName == accountName);

        if (passwordEntry is null)
            throw new PasswordNotFoundException(serviceName, accountName);

        return  _vaultEncryptionService.Decrypt(passwordEntry.EncryptedPassword, _masterPassword);
    }
    
    public bool UpdatePasswordEntry(string currentServiceName, string currentAccountName, string newServiceName, string newAccountName, int passwordLength, string? newPassword = null)
    {
        var passwordEntry = _passwordEntries.FirstOrDefault(x =>
            x.ServiceName == currentServiceName && x.AccountName == currentAccountName);

        if (passwordEntry == null)
            return false;

        passwordEntry.ServiceName = newServiceName;
        passwordEntry.AccountName = newAccountName;

        // If newPassword is null, generate a random password
        passwordEntry.EncryptedPassword = _vaultEncryptionService.Encrypt(newPassword ?? _vaultPasswordGenerator.GeneratePassword(passwordLength), _masterPassword);

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
        var password = _vaultPasswordGenerator.GeneratePassword(passwordLength);  // Generate a password
        AddPasswordEntry(serviceName, accountName, password);  // Add it to password entries
        return password;
    }
}