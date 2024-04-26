namespace KeyVaultCli.Domain.Entities;

public class VaultBackup
{
    public MetaData Metadata { get; set; }
    public List<PasswordEntry> Passwords { get; set; }
}