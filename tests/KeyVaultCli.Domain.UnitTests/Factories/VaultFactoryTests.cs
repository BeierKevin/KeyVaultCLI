using KeyVaultCli.Domain.Common.Interfaces;
using KeyVaultCli.Domain.Factories;
using KeyVaultCli.Domain.UnitTests.Fakes;

namespace KeyVaultCli.Domain.UnitTests.Factories;

[TestClass]
public class VaultFactoryTests
{
    private VaultFactory _vaultFactory;

    [TestInitialize]
    public void TestInitialize()
    {
        var encryptionService = new FakeEncryptionService();
        var fileService = new FakeFileService();
        var passwordGenerator = new FakePasswordGeneratorService();
        var consoleService = new FakeConsoleService();
        _vaultFactory = new VaultFactory(consoleService, encryptionService, fileService, passwordGenerator);
    }

    [TestMethod]
    public void TestCreateVault_withEmptyMasterPassword_ReturnsNull()
    {
        var result = _vaultFactory.CreateVault(string.Empty);

        Assert.IsNull(result, "Should not create a Vault instance if the master password is empty.");
    }

    [TestMethod]
    public void TestCreateVault_withValidMasterPassword_ReturnsVault()
    {
        var result = _vaultFactory.CreateVault("myMasterPassword");

        Assert.IsInstanceOfType(result, typeof(IVault),
            "Should not create a Vault instance if the master password is incorrect.");
    }
}