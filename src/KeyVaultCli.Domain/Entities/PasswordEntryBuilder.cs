namespace KeyVaultCli.Domain.Entities;

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

    public PasswordEntry Build()
    {
        return _passwordEntry;
    }
}