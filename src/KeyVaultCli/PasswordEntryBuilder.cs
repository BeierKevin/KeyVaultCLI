namespace CLI;

// Builder Pattern
public class PasswordEntryBuilder
{
    private readonly PasswordEntry _passwordEntry = new();

    public PasswordEntryBuilder SetServiceName(string serviceName)
    {
        _passwordEntry.ServiceName = serviceName;
        return this;
    }

    public PasswordEntryBuilder SetAccountName(string accountName)
    {
        _passwordEntry.AccountName = accountName;
        return this;
    }

    public PasswordEntryBuilder SetEncryptedPassword(string encryptedPassword)
    {
        _passwordEntry.EncryptedPassword = encryptedPassword;
        return this;
    }

    public PasswordEntry Build()
    {
        return _passwordEntry;
    }
}