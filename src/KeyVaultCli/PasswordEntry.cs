namespace KeyVaultCli;

//Builder Pattern: If creating an instance of PasswordEntry involves setting more complex properties, or if there are many permutations of properties that could be set, you might want to introduce a builder for PasswordEntry. For example:
// var passwordEntry = new PasswordEntryBuilder() .SetServiceName(serviceName) .SetAccountName(accountName) .SetEncryptedPassword(encryptedPassword) .Build();
public class PasswordEntry
{
    public string Name { get; set; }
    public string Password { get; set; }
    public string Category { get; set; }
    public string ServiceName { get; set; }
    public string AccountName { get; set; }
    public string EncryptedPassword { get; set; }
}