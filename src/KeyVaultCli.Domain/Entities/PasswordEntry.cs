namespace KeyVaultCli.Domain.Entities;

//Builder Pattern: If creating an instance of PasswordEntry involves setting more complex properties, or if there are many permutations of properties that could be set, you might want to introduce a builder for PasswordEntry. For example:
// var passwordEntry = new PasswordEntryBuilder() .SetServiceName(serviceName) .SetAccountName(accountName) .SetEncryptedPassword(encryptedPassword) .Build();
public class PasswordEntry
{
    public Guid EntryId { get; init; }
    public string ServiceName { get; set; }
    public string AccountName { get; set; }
    public string EncryptedPassword { get; set; }
    public string Url { get; set; }
    public string Category { get; set; }
    public DateTime CreationDate { get; init; }
    public DateTime LastModifiedDate { get; set; }

    public PasswordEntry()
    {
        EntryId = Guid.NewGuid();
        var berlinTimezone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
        var currentDateTimeInBerlin = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, berlinTimezone);
        CreationDate = currentDateTimeInBerlin;
        LastModifiedDate = currentDateTimeInBerlin;
    }

    public void UpdateServiceName(string newServiceName)
    {
        ServiceName = newServiceName;
        UpdateLastModifiedDate();
    }

    public void UpdateAccountName(string newAccountName)
    {
        AccountName = newAccountName;
        UpdateLastModifiedDate();
    }

    public void UpdateUrl(string newUrl)
    {
        Url = newUrl;
        UpdateLastModifiedDate();
    }

    public void UpdateCategory(string newCategory)
    {
        Category = newCategory;
        UpdateLastModifiedDate();
    }

    public void UpdateEncryptedPassword(string newPassword)
    {
        EncryptedPassword = newPassword;
        UpdateLastModifiedDate();
    }

    private void UpdateLastModifiedDate()
    {
        var berlinTimezone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
        LastModifiedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, berlinTimezone);
    }
}