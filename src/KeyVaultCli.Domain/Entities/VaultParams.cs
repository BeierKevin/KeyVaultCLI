using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Domain.Entities;

public class VaultParams
{
    public string VaultFilePath { get; set; }
    public string MasterPasswordFilePath { get; set; }
    public string MasterPassword { get; set; }
    public IVaultEncryptionService VaultEncryptionService { get; set; }
    public IVaultFileService VaultFileService { get; set; }
    public IVaultPasswordGenerator VaultPasswordGenerator { get; set; }
}