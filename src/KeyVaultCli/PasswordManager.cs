using KeyVaultCli.Crypto;

namespace KeyVaultCli.Security.Passwords;

public class PasswordManager
{
    private readonly List<PasswordEntry> passwordEntries;
    private readonly Configuration config;

    public PasswordManager(Configuration config)
    {
        passwordEntries = new List<PasswordEntry>();
    }

    public void CreatePasswordEntry(string serviceName, string accountName, string password)
    {
        var passwordEntry = new PasswordEntry
        {
            ServiceName = serviceName,
            AccountName = accountName,
            EncryptedPassword = EncryptionHelper.Encrypt(password, config.MasterPassword)
        };

        passwordEntries.Add(passwordEntry);
    }

    public PasswordEntry ReadPasswordEntry(string serviceName, string accountName)
    {
        return passwordEntries.Find(entry =>
            entry.ServiceName.Equals(serviceName, StringComparison.OrdinalIgnoreCase) &&
            entry.AccountName.Equals(accountName, StringComparison.OrdinalIgnoreCase));
    }

    public void UpdatePasswordEntry(string serviceName, string accountName, string newPassword)
    {
        var existingEntry = ReadPasswordEntry(serviceName, accountName);

        if (existingEntry != null)
        {
            existingEntry.EncryptedPassword =
                EncryptionHelper.Encrypt(newPassword, config.MasterPassword);
        }
        else
        {
            Console.WriteLine("Password entry not found.");
        }
    }

    public void DeletePasswordEntry(string serviceName, string accountName)
    {
        var entryToRemove = ReadPasswordEntry(serviceName, accountName);

        if (entryToRemove != null)
        {
            passwordEntries.Remove(entryToRemove);
        }
        else
        {
            Console.WriteLine("Password entry not found.");
        }
    }
}

public class PasswordEntry
{
    public string ServiceName { get; set; }
    public string AccountName { get; set; }
    public string EncryptedPassword { get; set; }
}