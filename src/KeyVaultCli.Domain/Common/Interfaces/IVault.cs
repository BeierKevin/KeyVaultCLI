using KeyVaultCli.Domain.Entities;

namespace KeyVaultCli.Domain.Common.Interfaces;

public interface IVault
{
    public void SaveMasterPassword();

    public string? LoadMasterPassword();

    public bool UpdateMasterPassword(string oldPassword, string newPassword);

    public void AddPasswordEntry(string serviceName, string accountName, string password, string url, string category);

    public void SavePasswordEntries();

    public bool DeletePasswordEntry(string serviceName, string accountName);

    public void DeleteAllPasswordEntries();

    public List<PasswordEntry> LoadPasswordEntries();

    public Dictionary<string, string> GetAllDecryptedPasswords();

    public string GetPassword(string serviceName, string accountName);
    
    public PasswordEntry? GetPasswordEntry(string serviceName, string accountName);

    public bool UpdatePasswordEntry(string currentServiceName, string currentAccountName, string newServiceName,
        string newAccountName, int passwordLength, string? newPassword = null, string? newUrl = null,
        string? newCategory = null);

    public List<PasswordEntry> SearchPasswordEntries(string searchTerm);

    public string GenerateAndAddPasswordEntry(string serviceName, string accountName, int passwordLength, string url, string category);
    
    public bool BackupVault(string backupFilePath);
    
    public bool RestoreVault(string backupFilePath);
}