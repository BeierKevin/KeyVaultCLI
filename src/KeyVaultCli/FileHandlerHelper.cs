using KeyVaultCli.Crypto;

namespace KeyVaultCli.Security.IO;

public class FileHandler
{
    // Path to the file where passwords are stored
    private readonly string vaultFilePath;

    public FileHandler(string vaultFilePath)
    {
        this.vaultFilePath = vaultFilePath;
    }

    public bool VaultExists()
    {
        return File.Exists(vaultFilePath);
    }

    public void CreateVault()
    {
        // Implement logic to create the local Vault
        Console.WriteLine("Creating a new Vault...");
    }

    public string ReadEncryptedFile(Configuration config)
    {
        if (VaultExists())
        {
            try
            {
                // Read the encrypted content from the file
                return File.ReadAllText(vaultFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading the vault file: {ex.Message}");
                return null;
            }
        }
        else
        {
            Console.WriteLine("Vault does not exist. Create a new vault.");
            return null;
        }
    }

    public void WriteEncryptedFile(string encryptedContent)
    {
        try
        {
            // Write the encrypted content to the file
            File.WriteAllText(vaultFilePath, encryptedContent);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to the vault file: {ex.Message}");
        }
    }
}