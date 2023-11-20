using KeyVaultCli.Crypto;
using KeyVaultCli.Security.IO;
using KeyVaultCli.Security.Passwords;

namespace KeyVaultCli;

class Program
{
    private static void Main(string[] args)
    {
        var config = new Configuration();
        var fileHandler = new FileHandler(config.VaultFilePath);
        var passwordValidator = new MasterPasswordValidator(fileHandler);

        Run(config, fileHandler, passwordValidator);
    }

    private static void Run(Configuration config, FileHandler fileHandler, MasterPasswordValidator passwordValidator)
    {
        Console.WriteLine("Welcome to KeyVaultCli (kvc), your local password generator and manager.");
        var vaultHasBeenCreated = false;

        // 1. Check if local Vault exists
        if (!fileHandler.VaultExists())
        {
            // 2. If not, create it
            fileHandler.CreateVault();
            vaultHasBeenCreated = true;
        }

        Console.WriteLine($"Using vault file path: {config.VaultFilePath}");

        // 3. If yes, ask for master password
        Console.Write("Enter your master password: ");
        var enteredPassword = Console.ReadLine();

        // 4. If master password is correct, continue
        if (vaultHasBeenCreated || passwordValidator.IsMasterPasswordCorrect(config, enteredPassword))
        {
            var exitRequested = false;

            // Continue the loop until the user decides to exit
            while (!exitRequested)
            {
                // 6. If master password was correct, show menu
                ShowMenu(config);

                Console.Write("Enter your choice (0 to exit): ");
                var optionInput = Console.ReadLine();

                switch (optionInput)
                {
                    case "0":
                        exitRequested = true;
                        Console.WriteLine("Exiting...");
                        break;
                    case "1":
                        // Implement logic for option 1 (Generate a new password)
                        GenerateNewPassword(config);
                        break;
                    case "2":
                        // Implement logic for option 2 (List all passwords)
                        ListAllPasswords(config);
                        break;
                    // Add cases for other options as needed
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        else
        {
            Console.WriteLine("Incorrect master password. Exiting...");
        }
    }

    private static void ShowConfiguration(Configuration config)
    {
        // Display the entered configuration
        Console.WriteLine("Configuration:");
        Console.WriteLine($"Service Name: {config.ServiceName}");
        Console.WriteLine($"Common Name: {config.CommonName}");
        Console.WriteLine($"Master Password: {config.MasterPassword}");
        Console.WriteLine($"Has Numeric: {config.HasNumeric}");
        Console.WriteLine($"Has Letters: {config.HasLetters}");
        Console.WriteLine($"Has Special Symbols: {config.HasSpecialSymbols}");
        Console.WriteLine($"Password Length: {config.Length}");
    }

    private static void ShowMenu(Configuration config)
    {
        // Display menu options
        Console.WriteLine("Menu:");
        Console.WriteLine("1. Generate a new password");
        Console.WriteLine("2. List all passwords with services and accounts");
        Console.WriteLine("3. Search for something");
        Console.WriteLine("4. Update master password");
        Console.WriteLine("0. Exit");

        // Get user input
        Console.Write("Enter your choice: ");
        var userInput = Console.ReadLine();

        // Process user input
        switch (userInput)
        {
            case "1":
                GenerateNewPassword(config);
                break;
            case "2":
                ListAllPasswords(config);
                break;
            case "3":
                SearchPasswords(config);
                break;
            case "4":
                UpdateMasterPassword(config);
                break;
            case "0":
                Console.WriteLine("Exiting...");
                break;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                ShowMenu(config); // Show the menu again for a valid choice
                break;
        }
    }

    private static void GenerateNewPassword(Configuration config)
    {
        // Use the PasswordGenerator to generate a new strong password
        var generatedPassword = PasswordGenerator.GeneratePassword(config.Length, config.HasNumeric, config.HasLetters,
            config.HasSpecialSymbols);

        // Display the generated password
        Console.WriteLine($"Generated Password: {generatedPassword}");

        // Example: Encrypt the generated password
        var encryptedPassword = EncryptionHelper.Encrypt(generatedPassword, config.MasterPassword);
        Console.WriteLine($"Encrypted Password: {encryptedPassword}");
    }

    private static void PasswordActions()
    {
        Console.WriteLine("Menu:");
        Console.WriteLine("S. Show Password");
        Console.WriteLine("E. Edit Password");
        Console.WriteLine("D. Delete Password");
        Console.WriteLine("0. Exit");
    }

    private static void ListAllPasswords(Configuration config)
    {
        // Implement logic to list all passwords
        Console.WriteLine("Listing all passwords...");

        // Example: Decrypt a stored password
        var encryptedPassword = "YourEncryptedPassword";
        var decryptedPassword = EncryptionHelper.Decrypt(encryptedPassword, config.MasterPassword);
        Console.WriteLine($"Decrypted Password: {decryptedPassword}");
        PasswordActions();
        var t = Console.ReadLine();

        if (t == "E")
        {
            UpdatePasswordEntry(config);
        }
    }

    private static void SearchPasswords(Configuration config)
    {
        // Implement logic to search for passwords
        Console.WriteLine("Searching for passwords...");
    }

    private static void UpdateMasterPassword(Configuration config)
    {
        // Implement logic to update the master password
        Console.WriteLine("Updating master password...");

        // Example: Decrypt and then encrypt a password with the new master password
        var encryptedPassword = "YourEncryptedPassword";
        var decryptedPassword = EncryptionHelper.Decrypt(encryptedPassword, config.MasterPassword);

        Console.Write("Enter the new master password: ");
        var newMasterPassword = Console.ReadLine();

        var reEncryptedPassword = EncryptionHelper.Encrypt(decryptedPassword, newMasterPassword);
        Console.WriteLine($"Re-encrypted Password with the new master password: {reEncryptedPassword}");
    }

    private static void CreatePasswordEntry(Configuration config)
    {
        Console.Write("Enter service name: ");
        string serviceName = Console.ReadLine();

        Console.Write("Enter account name: ");
        string accountName = Console.ReadLine();

        Console.Write("Enter password: ");
        string password = Console.ReadLine();

        // Use the PasswordManager to create a new password entry
        var passwordManager = new PasswordManager(config);
        passwordManager.CreatePasswordEntry(serviceName, accountName, password);

        Console.WriteLine("Password entry created successfully.");
    }

    private static void ReadPasswordEntry(Configuration config)
    {
        Console.Write("Enter service name: ");
        string serviceName = Console.ReadLine();

        Console.Write("Enter account name: ");
        string accountName = Console.ReadLine();

        // Use the PasswordManager to read a password entry
        var passwordManager = new PasswordManager(config);
        var passwordEntry = passwordManager.ReadPasswordEntry(serviceName, accountName);

        if (passwordEntry != null)
        {
            // Decrypt and display the password
            var decryptedPassword = EncryptionHelper.Decrypt(passwordEntry.EncryptedPassword, config.MasterPassword);
            Console.WriteLine($"Decrypted Password: {decryptedPassword}");
        }
        else
        {
            Console.WriteLine("Password entry not found.");
        }
    }

    private static void UpdatePasswordEntry(Configuration config)
    {
        Console.Write("Enter service name: ");
        string serviceName = Console.ReadLine();

        Console.Write("Enter account name: ");
        string accountName = Console.ReadLine();

        Console.Write("Enter new password: ");
        string newPassword = Console.ReadLine();

        // Use the PasswordManager to update a password entry
        var passwordManager = new PasswordManager(config);
        passwordManager.UpdatePasswordEntry(serviceName, accountName, newPassword);

        Console.WriteLine("Password entry updated successfully.");
    }

    private static void DeletePasswordEntry(Configuration config)
    {
        Console.Write("Enter service name: ");
        string serviceName = Console.ReadLine();

        Console.Write("Enter account name: ");
        string accountName = Console.ReadLine();

        // Use the PasswordManager to delete a password entry
        var passwordManager = new PasswordManager(config);
        passwordManager.DeletePasswordEntry(serviceName, accountName);

        Console.WriteLine("Password entry deleted successfully.");
    }
}