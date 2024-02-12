using KeyVaultCli.Domain.Entities;
using KeyVaultCli.Domain.Factories;
using KeyVaultCli.Infrastructure.Cryptography;
using KeyVaultCli.Infrastructure.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KeyVaultCli.Domain.UnitTests;

[TestClass]
public class VaultFactoryTests
{
    private VaultFactory _vaultFactory;

    [TestInitialize]
    public void TestInitialize()
    {
        var encryptionService = new EncryptionService();
        var fileService = new FileService();
        var passwordGenerator = new PasswordGenerator();
        var consoleService = new ConsoleService();
        _vaultFactory = new VaultFactory(consoleService, encryptionService, fileService, passwordGenerator);
    }

    [TestMethod]
    public void TestCreateVault_withEmptyMasterPassword_ReturnsNull()
    {
        var result = _vaultFactory.CreateVault(string.Empty);

        Assert.IsNull(result, "Should not create a Vault instance if the master password is empty.");
    }

    [TestMethod]
    public void TestCreateVault_withInvalidMasterPassword_ReturnsNull()
    {
        // Assuming LoadMasterPassword returns "existingMasterPassword"
        var result = _vaultFactory.CreateVault("wrongMasterPassword");

        Assert.IsNull(result, "Should not create a Vault instance if the master password is incorrect.");
    }

    [TestMethod]
    public void TestCreateVault_withValidMasterPassword_ReturnsVault()
    {
        var result = _vaultFactory.CreateVault("correctMasterPassword");

        Assert.IsInstanceOfType(result, typeof(Vault),
            "Should create a Vault instance if the master password is correct.");
    }
}