using KeyVaultCli.Domain.Common.Interfaces;
using KeyVaultCli.Domain.Entities;

namespace KeyVaultCli.Domain.Factories;

// Factory Pattern
public class VaultFactory(
    IConsole consoleService,
    IVaultEncryptionService encryptionService,
    IVaultFileService fileService,
    IVaultPasswordGenerator passwordGenerator)
    : IVaultFactory
{
    private const string VaultFilePath = "vault.dat";
    private const string MasterPasswordFilePath = "masterpassword.dat";
    private IVault _vault;

    private readonly IConsole vaultConsoleService =
        consoleService ?? throw new ArgumentNullException(nameof(consoleService));

    private readonly IVaultEncryptionService vaultEncryptionService =
        encryptionService ?? throw new ArgumentNullException(nameof(encryptionService));

    private readonly IVaultFileService vaultFileService =
        fileService ?? throw new ArgumentNullException(nameof(fileService));

    private readonly IVaultPasswordGenerator vaultPasswordGenerator =
        passwordGenerator ?? throw new ArgumentNullException(nameof(passwordGenerator));


    public IVault? CreateVault(string masterPassword)
    {
        if (string.IsNullOrEmpty(masterPassword))
        {
            vaultConsoleService.WriteError("Master password should not be empty");
            return null;
        }

        _vault = new Vault(VaultFilePath, MasterPasswordFilePath, masterPassword, vaultEncryptionService,
            vaultFileService, vaultPasswordGenerator);
        var savedPassword = _vault.LoadMasterPassword();
        if (savedPassword == null)
        {
            _vault.SaveMasterPassword();
        }
        else if (savedPassword != masterPassword)
        {
            vaultConsoleService.WriteError("Invalid master password. Exit.");
            return null;
        }

        return _vault;
    }

    public bool DeleteVault()
    {
        var isVaultDeleted = vaultFileService.Delete(VaultFilePath);
        var isMasterPasswordDeleted = vaultFileService.Delete(MasterPasswordFilePath);
        var deleted = isVaultDeleted && isMasterPasswordDeleted;
        return deleted;
    }

    public IVault GetVault()
    {
        return _vault;
    }
}