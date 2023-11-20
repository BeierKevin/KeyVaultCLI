using KeyVaultCli.Crypto;
using KeyVaultCli.Security.IO;

namespace KeyVaultCli.Security.Passwords;

public class MasterPasswordValidator
{
    private readonly FileHandler fileHandler;

    public MasterPasswordValidator(FileHandler fileHandler)
    {
        this.fileHandler = fileHandler;
    }

    public bool IsMasterPasswordCorrect(Configuration config, string enteredPassword)
    {
        // Read the encrypted file content
        var encryptedContent = fileHandler.ReadEncryptedFile(config);

        if (encryptedContent != null)
        {
            // Decrypt the content using the entered password
            var decryptedContent = EncryptionHelper.Decrypt(encryptedContent, enteredPassword);

            // If decryption is successful, the master password is correct
            return decryptedContent != null;
        }

        return false;
    }
}