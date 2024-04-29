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

    public Vault(VaultParams vaultParams)
    {
        _passwordEntryFilePath = vaultParams.VaultFilePath;
        _masterPasswordFilePath = vaultParams.MasterPasswordFilePath;
        _masterPassword = vaultParams.MasterPassword;
        _vaultEncryptionService = vaultParams.VaultEncryptionService;
        _vaultFileService = vaultParams.VaultFileService;
        _vaultPasswordGenerator = vaultParams.VaultPasswordGenerator;
        _passwordEntries = LoadPasswordEntries();
    }

    public void AddEntryToPasswordList(string serviceName, string accountName, string password, string url,
        string category)
    {
        var encryptedPassword = _vaultEncryptionService.Encrypt(password, _masterPassword);

        var passwordEntryBuilder = new PasswordEntryBuilder();
        passwordEntryBuilder.SetServiceName(serviceName)
            .SetAccountName(accountName)
            .SetEncryptedPassword(encryptedPassword)
            .SetUrl(url)
            .SetCategory(category);

        var entry = passwordEntryBuilder.Build();

        _passwordEntries.Add(entry);
        SavePasswordEntries();
    }

    public string DecryptAndRetrievePassword(string serviceName, string accountName)
    {
        if (serviceName is null || accountName is null)
            throw new ArgumentNullException(serviceName is null ? nameof(serviceName) : nameof(accountName));

        var passwordEntry = _passwordEntries.FirstOrDefault(entry =>
            entry.ServiceName == serviceName && entry.AccountName == accountName);

        if (passwordEntry is null)
            throw new PasswordNotFoundException(serviceName, accountName);

        return _vaultEncryptionService.Decrypt(passwordEntry.EncryptedPassword, _masterPassword);
    }

    public bool UpdateAndSavePasswordEntry(string currentServiceName, string currentAccountName, string newServiceName,
        string newAccountName, int passwordLength, string? newPassword = null, string? newUrl = null,
        string? newCategory = null)
    {
        var passwordEntry = _passwordEntries.FirstOrDefault(x =>
            x.ServiceName == currentServiceName && x.AccountName == currentAccountName);

        if (passwordEntry == null)
            return false;

        passwordEntry.UpdateServiceName(newServiceName);
        passwordEntry.UpdateAccountName(newAccountName);

        // Updating url and category
        if (newUrl != null)
            passwordEntry.UpdateUrl(newUrl);

        if (newCategory != null)
            passwordEntry.UpdateCategory(newCategory);

        // If newPassword is null, generate a random password
        var rand = new Random();
        passwordEntry.UpdateEncryptedPassword(_vaultEncryptionService.Encrypt(newPassword ?? _vaultPasswordGenerator
            .GeneratePassword(passwordLength), _masterPassword));

        SavePasswordEntries();
        return true;
    }

    public string GeneratePasswordAndAddEntry(string serviceName, string accountName, int passwordLength,
        string url = "", string category = "")
    {
        var newPass = _vaultPasswordGenerator.GeneratePassword(passwordLength);
        AddEntryToPasswordList(serviceName, accountName, newPass, url, category);
        return newPass;
    }


    public void SaveMasterPassword()
    {
        var encryptedPassword = _vaultEncryptionService.Encrypt(this._masterPassword, "staticEncryptionKey");
        _vaultFileService.WriteAllText(_masterPasswordFilePath, encryptedPassword);
    }

    public string? LoadMasterPassword()
    {
        if (!_vaultFileService.Exists(_masterPasswordFilePath)) return null;
        var encryptedPassword = _vaultFileService.ReadAllText(_masterPasswordFilePath);
        return _vaultEncryptionService.Decrypt(encryptedPassword, "staticEncryptionKey");
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

    public Dictionary<string, string> GetAllDecryptedPasswords()
    {
        var result = new Dictionary<string, string>();

        foreach (var entry in _passwordEntries)
        {
            var decryptedPassword = DecryptAndRetrievePassword(entry.ServiceName, entry.AccountName);
            result[$"{entry.ServiceName}/{entry.AccountName}"] = decryptedPassword;
        }

        return result;
    }


    public PasswordEntry? GetPasswordEntry(string serviceName, string accountName)
    {
        return _passwordEntries.FirstOrDefault(x => x.ServiceName == serviceName && x.AccountName == accountName);
    }


    public List<PasswordEntry> SearchPasswordEntries(string searchTerm)
    {
        return _passwordEntries.Where(x =>
            x?.ServiceName?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) == true
            || x?.AccountName?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) == true
            || x?.Url?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) == true
            || x?.Category?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) == true
            || x?.EntryId.ToString().Contains(searchTerm, StringComparison.OrdinalIgnoreCase) == true
        ).ToList();
    }


    public bool BackupVault(string backupFilePath)
    {
        try
        {
            // Description for your vault
            string description = "Personal password vault";

            // Define an intermediary vault representation 
            var vaultBackup = new VaultBackup
            {
                Metadata = new MetaData
                {
                    Description = description,
                    BackupCreatedDate = DateTime.UtcNow.ToString("O")
                },
                Passwords = this._passwordEntries.Select(entry => new PasswordEntry
                {
                    EntryId = entry.EntryId,
                    ServiceName = entry.ServiceName,
                    AccountName = entry.AccountName,
                    EncryptedPassword = DecryptAndRetrievePassword(entry.ServiceName, entry.AccountName),
                    CreationDate = entry.CreationDate,
                    LastModifiedDate = entry.LastModifiedDate,
                    // URL = entry.URL
                }).ToList()
            };

            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(vaultBackup, options);

            // Define a default file name
            var defaultFileName = "VaultBackup.json";

            // Combine path and filename
            var fullBackupFilePath = Path.Combine(backupFilePath, defaultFileName);

            File.WriteAllText(fullBackupFilePath, jsonString);
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error occurred while backing up the vault: {ex.Message}");
        }
    }

    public bool RestoreVault(string backupFilePath)
    {
        try
        {
            var jsonString = File.ReadAllText(backupFilePath);
            var restoredVault = JsonSerializer.Deserialize<VaultBackup>(jsonString);

            // Remove current password entries
            this._passwordEntries.Clear();

            // If restored password entries exist, add them to the vault. 
            // Avoids possible null reference argument.
            if (restoredVault?.Passwords != null)
            {
                this._passwordEntries.AddRange(restoredVault.Passwords);
            }

            // Now, restoredVault.MetaData is available and you may use it as needed...

            SavePasswordEntries(); // Save entries to current vault file

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error occurred while restoring the vault: {ex.Message}");
        }
    }
}