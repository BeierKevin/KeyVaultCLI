using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;
using KeyVaultCli.Domain.Factories;

namespace KeyVaultCli.Application.Services;

public class VaultService() : IVaultService
{
    private readonly IConsole _vaultConsoleService;
    private readonly IVaultEncryptionService _vaultEncryptionService;
    private readonly IVaultFileService _vaultFileService;
    private readonly IVaultPasswordGenerator _vaultPasswordGenerator;
    private VaultFactory _vaultFactory;

    public VaultService(IConsole vaultConsoleService,
        IVaultEncryptionService vaultEncryptionService,
        IVaultFileService vaultFileService, IVaultPasswordGenerator vaultPasswordGenerator) : this()
    {
        _vaultConsoleService = vaultConsoleService;
        _vaultEncryptionService = vaultEncryptionService;
        _vaultFileService = vaultFileService;
        _vaultPasswordGenerator = vaultPasswordGenerator;
        _vaultFactory = new VaultFactory(_vaultConsoleService, _vaultEncryptionService, _vaultFileService, _vaultPasswordGenerator);
    }
    
    public IVault? CreateVault(string masterPassword)
    {
        return _vaultFactory.CreateVault(masterPassword);
    }

    public bool DeleteVault()
    {
        var isDeleted = _vaultFactory.DeleteVault();
        return isDeleted;
    }
    
    public IVault GetVault()
    {
        return _vaultFactory.GetVault();
    }
}