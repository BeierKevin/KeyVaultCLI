using KeyVaultCli.Domain.Entities;

namespace KeyVaultCli.Domain.Factories;

// Builder Pattern
public class PasswordEntryBuilder
{
    private readonly PasswordEntry _passwordEntry = new()
    {
        EntryId = Guid.NewGuid(),
        CreationDate = DateTime.UtcNow,
        LastModifiedDate = DateTime.UtcNow
    };

    public PasswordEntryBuilder SetServiceName(string serviceName)
    {
        _passwordEntry.ServiceName = serviceName;
        _passwordEntry.LastModifiedDate = DateTime.UtcNow;
        return this;
    }

    public PasswordEntryBuilder SetAccountName(string accountName)
    {
        _passwordEntry.AccountName = accountName;
        _passwordEntry.LastModifiedDate = DateTime.UtcNow;
        return this;
    }

    public PasswordEntryBuilder SetEncryptedPassword(string encryptedPassword)
    {
        _passwordEntry.EncryptedPassword = encryptedPassword;
        _passwordEntry.LastModifiedDate = DateTime.UtcNow;
        return this;
    }

    public PasswordEntryBuilder SetUrl(string url)
    {
        _passwordEntry.Url = url;
        _passwordEntry.LastModifiedDate = DateTime.UtcNow;
        return this;
    }

    public PasswordEntryBuilder SetCategory(string category)
    {
        _passwordEntry.Category = category;
        _passwordEntry.LastModifiedDate = DateTime.UtcNow;
        return this;
    }

    public PasswordEntry Build()
    {
        return _passwordEntry;
    }
}