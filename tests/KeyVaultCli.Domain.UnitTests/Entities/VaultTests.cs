using KeyVaultCli.Domain.Entities;
using KeyVaultCli.Domain.UnitTests.Fakes;

namespace KeyVaultCli.Domain.UnitTests.Entities;

[TestClass]
public class VaultTests
{
    private Vault _vault;
    private const string VaultFilePath = "vault.dat";
    private const string MasterPasswordFilePath = "masterpassword.dat";

    [TestInitialize]
    public void TestInitialize()
    {
        var encryptionService = new FakeEncryptionService();
        var fileService = new FakeFileService();
        var passwordGenerator = new FakePasswordGeneratorService();
        _vault = new Vault(VaultFilePath, MasterPasswordFilePath, "masterPassword", encryptionService, fileService,
            passwordGenerator);
    }

    [TestMethod]
    public void TestUpdateMasterPassword_Success()
    {
        var result = _vault.UpdateMasterPassword("masterPassword", "newMasterPassword");

        Assert.IsTrue(result, "Master password should have been updated successfully.");
    }

    [TestMethod]
    public void TestAddPasswordEntry_Success()
    {
        _vault.DeleteAllPasswordEntries();
        _vault.AddPasswordEntry("testService", "testAccount", "testPassword", "testUrl", "testCategory");
        var password = _vault.GetPassword("testService", "testAccount");

        Assert.AreEqual("testPassword", password, "Password should have been stored successfully.");
    }

    [TestMethod]
    public void TestDeletePasswordEntry_Success()
    {
        _vault.AddPasswordEntry("testService", "testAccount", "testPassword", "testUrl", "testCategory");
        var result = _vault.DeletePasswordEntry("testService", "testAccount");

        Assert.IsTrue(result, "Password entry should have been deleted successfully.");
    }

    [TestMethod]
    public void TestUpdatePasswordEntry_Success()
    {
        _vault.AddPasswordEntry("testService", "testAccount", "testPassword", "testUrl", "testCategory");
        var result =
            _vault.UpdatePasswordEntry("testService", "testAccount", "newService", "newAccount", 10, "newPassword");
        var password = _vault.GetPassword("newService", "newAccount");

        Assert.IsTrue(result, "Password entry should have been updated successfully.");
        Assert.AreEqual("newPassword", password, "Password should have been updated successfully.");
    }

    [TestMethod]
    public void TestDeleteAllPasswordEntries_Success()
    {
        _vault.AddPasswordEntry("testService1", "testAccount1", "testPassword1", "testUrl1", "testCategory1");
        _vault.AddPasswordEntry("testService2", "testAccount2", "testPassword2", "testUrl2", "testCategory2");

        _vault.DeleteAllPasswordEntries();
        var entries = _vault.LoadPasswordEntries();

        Assert.AreEqual(0, entries.Count, "All password entries should have been deleted.");
    }

    [TestMethod]
    public void TestGenerateAndAddPasswordEntry_Success()
    {
        string password = _vault.GenerateAndAddPasswordEntry("testService", "testAccount", 10);

        Assert.IsNotNull(password, "Generated password should not be null.");
        Assert.AreEqual(10, password.Length, "Generated password length should be 10.");
    }

    [TestMethod]
    public void TestSearchPasswordEntries_Success()
    {
        _vault.AddPasswordEntry("service1", "account1", "password1", "url1", "category1");
        _vault.AddPasswordEntry("service2", "account2", "password2", "url2", "category2");
        _vault.AddPasswordEntry("testService3", "testAccount3", "testPassword3", "testUrl3", "testCategory3");

        var expectedResult1 = _vault.SearchPasswordEntries("service1");
        var expectedResult2 = _vault.SearchPasswordEntries("testService3");

        Assert.AreEqual("service1", expectedResult1.First().ServiceName,
            "Searched password entry should match the service name.");
        Assert.AreEqual("testService3", expectedResult2.First().ServiceName,
            "Searched password entry should match the service name.");
    }
}