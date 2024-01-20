using System.Runtime.Serialization.Formatters.Binary;
using KeyVaultCli.Crypto;
using KeyVaultCli.Security.IO;
using KeyVaultCli.Security.Passwords;
using System.Text;
using KeyVaultCli.Core;

namespace KeyVaultCli;

class Program
{
    private static void Main(string[] args)
    {
        Console.Write("Enter your master password: ");
        var masterPassword = Console.ReadLine();
        
        if (masterPassword == null) return;
        var vault = new Vault(masterPassword);
        var savedPassword = vault.LoadMasterPassword();
        if(savedPassword == null)
        {
            vault.SaveMasterPassword();
        }
        else if(savedPassword != masterPassword)
        {
            Console.WriteLine("Invalid master password. Exit.");
            return;
        }

        while (true)
        {
            Console.WriteLine("1. Add password");
            Console.WriteLine("1-g. Generate and add password");
            Console.WriteLine("2. Get password");
            Console.WriteLine("3. Get all password");
            Console.WriteLine("4. Delete Password");
            Console.WriteLine("5. Update password");
            Console.WriteLine("5-g. Update password details with generated password");
            Console.WriteLine("6. Search password entries");
            Console.WriteLine("0. Exit");
            Console.WriteLine("-1. Delete all Passwords");
            Console.Write("Enter your choice: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter service name: ");
                    var serviceName = Console.ReadLine();
                    Console.Write("Enter account name: ");
                    var accountName = Console.ReadLine();
                    Console.Write("Enter password: ");
                    var password = Console.ReadLine();
                    vault.AddPasswordEntry(serviceName, accountName, password);
                    break;
                case "1-g":
                    Console.Write("Enter service name for the new password: ");
                    var gServiceName = Console.ReadLine();
                    Console.Write("Enter account name for the new password: ");
                    var gAccountName = Console.ReadLine();
                    Console.Write("Enter the desired password length (e.g. 10): ");
                    if (int.TryParse(Console.ReadLine(), out int passwordLength))
                    {
                        var gNewPassword = vault.GenerateAndAddPasswordEntry(gServiceName, gAccountName, 
                            passwordLength);
                        Console.WriteLine($"Generated and added a new password for service {gServiceName}, account {gAccountName}: {gNewPassword}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid password length. Please enter a valid number.");
                    }
                    break;
                case "2":
                    Console.Write("Enter service name: ");
                    serviceName = Console.ReadLine();
                    Console.Write("Enter account name: ");
                    accountName = Console.ReadLine();
                    password = vault.GetPassword(serviceName, accountName);
                    if (password != null)
                    {
                        Console.WriteLine($"Password: {password}");
                    }
                    else
                    {
                        Console.WriteLine("No password found for the given service and account name.");
                    }

                    break;
                case "3":
                    var allPasswords = vault.LoadPasswordEntries();
                    foreach(var passwordEntry in allPasswords)
                    {
                        Console.WriteLine("Service Name: " + passwordEntry.ServiceName);
                        Console.WriteLine("Account Name: " + passwordEntry.AccountName);
                        Console.WriteLine("Password " + vault.GetPassword(passwordEntry.ServiceName, passwordEntry.AccountName));
                        Console.WriteLine("-----------------------");
                    }
                    break;
                case "4":
                    Console.Write("Enter service name of the password to delete: ");
                    var deletableServiceName = Console.ReadLine();
                    Console.Write("Enter account name of the password to delete: ");
                    var deletableAccountName = Console.ReadLine();
                    vault.DeletePasswordEntry(deletableServiceName, deletableAccountName);
                    Console.WriteLine($"Deleted password for service: {deletableServiceName} and account: {deletableAccountName}");
                    break;
                case "5":
                    Console.Write("Enter current service name: ");
                    var currentServiceName = Console.ReadLine();
                    Console.Write("Enter current account name: ");
                    var currentAccountName = Console.ReadLine();
                    Console.Write("Enter new service name: ");
                    var newServiceName = Console.ReadLine();
                    Console.Write("Enter new account name: ");
                    var newAccountName = Console.ReadLine();
                    Console.Write("Enter new password: ");
                    var newPassword = Console.ReadLine();

                    if (vault.UpdatePasswordEntry(currentServiceName, currentAccountName, newServiceName, newAccountName, newPassword.Length, newPassword))
                        Console.WriteLine("Entry was updated successfully.");
                    else
                        Console.WriteLine("Error: Could not find the entry to be updated.");

                    break;
                case "5-g": // handling the new 'Update password details with generated password' option
                    Console.Write("Enter current service name: ");
                    var gCurrentServiceName = Console.ReadLine();
                    Console.Write("Enter current account name: ");
                    var gCurrentAccountName = Console.ReadLine();
                    Console.Write("Enter new service name: ");
                    var gNewServiceName = Console.ReadLine();
                    Console.Write("Enter new account name: ");
                    var gNewAccountName = Console.ReadLine();
                    Console.Write("Enter the desired password length: ");
                    var gPasswordLength = int.Parse(Console.ReadLine());

                    if (vault.UpdatePasswordEntry(gCurrentServiceName, gCurrentAccountName, gNewServiceName, gNewAccountName, gPasswordLength))
                        Console.WriteLine("Entry was updated successfully.");
                    else
                        Console.WriteLine("Error: Could not find the entry to be updated.");

                    break;
                case "6":
                    Console.Write("Enter search term: ");
                    var searchTerm = Console.ReadLine();
                    var results = vault.SearchPasswordEntries(searchTerm);
                    foreach(var passwordEntry in results)
                    {
                        Console.WriteLine($"Service Name: {passwordEntry.ServiceName}\nAccount Name: {passwordEntry.AccountName}");
                    }
                    break;
                case "-1":
                    Console.Write("Are you sure you want to delete all passwords? (y/n): ");
                    var confirmation = Console.ReadLine();
                    if (confirmation?.ToLower() == "y")
                    {
                        vault.DeleteAllPasswordEntries();
                        Console.WriteLine("All passwords have been deleted!");
                    }
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    break;
            }
        }
    }
}