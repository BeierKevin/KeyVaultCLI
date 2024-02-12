using KeyVaultCli.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KeyVaultCli.Domain.UnitTests.Entities;

[TestClass]
public class PasswordEntryBuilderTests
{
    [TestMethod]
    public void TestPasswordEntryCreation()
    {
        var serviceName = "TestService";
        var accountName = "TestAccount";
        var encryptedPassword = "TestPassword";

        var builder = new PasswordEntryBuilder()
            .SetServiceName(serviceName)
            .SetAccountName(accountName)
            .SetEncryptedPassword(encryptedPassword);

        PasswordEntry entry = builder.Build();

        Assert.AreEqual(serviceName, entry.ServiceName);
        Assert.AreEqual(accountName, entry.AccountName);
        Assert.AreEqual(encryptedPassword, entry.EncryptedPassword);
    }
}