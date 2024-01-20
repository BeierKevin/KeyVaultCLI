namespace KeyVaultCli.Core;

public class PasswordEntry
{
    public string Name { get; set; }
    public string Password { get; set; }
    public string Category { get; set; }
    public string ServiceName { get; set; }
    public string AccountName { get; set; }
    public string EncryptedPassword { get; set; }
}