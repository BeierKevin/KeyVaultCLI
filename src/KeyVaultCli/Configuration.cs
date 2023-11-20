namespace KeyVaultCli;

public class Configuration
{
    public string ServiceName { get; set; }
    public string CommonName { get; set; }
    public string MasterPassword { get; set; }
    public bool HasNumeric { get; set; } = true;
    public bool HasLetters { get; set; } = true;
    public bool HasSpecialSymbols { get; set; } = true;
    public int Length { get; set; } = 16;

    // Path to the file where passwords are stored
    public string VaultFilePath { get; set; } = "key-vault-cli.dat";


    public Configuration()
    {
    }
}