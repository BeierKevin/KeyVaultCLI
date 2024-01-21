namespace KeyVaultCli.Domain.Entities;

//Builder Pattern: If creating an instance of PasswordEntry involves setting more complex properties, or if there are many permutations of properties that could be set, you might want to introduce a builder for PasswordEntry. For example:
// var passwordEntry = new PasswordEntryBuilder() .SetServiceName(serviceName) .SetAccountName(accountName) .SetEncryptedPassword(encryptedPassword) .Build();
public class PasswordEntry
{
    public Guid EntryId { get; set; }
    public string ServiceName { get; set; }
    public string AccountName { get; set; }
    public string EncryptedPassword { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime LastModifiedDate { get; set; }

    public PasswordEntry()
    {
        EntryId = Guid.NewGuid();
        CreationDate = DateTime.UtcNow;
        LastModifiedDate = DateTime.UtcNow;
    }

    public void UpdateServiceName(string newServiceName)
    {
        ServiceName = newServiceName;
        LastModifiedDate = DateTime.UtcNow;
    }

    public void UpdateAccountName(string newAccountName)
    {
        AccountName = newAccountName;
        LastModifiedDate = DateTime.UtcNow;
    }

    public void UpdateEncryptedPassword(string newPassword)
    {
        EncryptedPassword = newPassword;
        LastModifiedDate = DateTime.UtcNow;
    }
}