using KeyVaultCli.Commands;
using KeyVaultCli.Core;

namespace KeyVaultCli;

class Program
{
    private static void Main(string[] args)
    {
        Console.Write("Enter your master password: ");
        var masterPassword = Console.ReadLine();
        var vault = VaultFactory.CreateVault(masterPassword);

        if(vault == null) return;  // Check if creation was successful
        
        // Command Pattern
        var commands = new Dictionary<string, ICommand>
        {
            { CommandFlags.CreatePassword, new CreatePasswordCommand(vault) },
            { CommandFlags.CreatePasswordGenerated, new CreatePasswordGenerateCommand(vault) },
            { CommandFlags.GetPassword, new GetPasswordCommand(vault) },
            { CommandFlags.GetAllPasswords, new GetAllPasswordsCommand(vault) },
            { CommandFlags.UpdatePassword, new UpdatePasswordCommand(vault) },
            { CommandFlags.UpdatePasswordGenerated, new UpdatePasswordGeneratedCommand(vault) },
            { CommandFlags.DeletePassword, new DeletePasswordCommand(vault) },
            { CommandFlags.SearchPasswordEntries, new SearchPasswordEntriesCommand(vault) },
            { CommandFlags.UpdateMasterPassword, new UpdateMasterPasswordCommand(vault) },
            { CommandFlags.Exit, new ExitCommand() },
            { CommandFlags.DeleteAllPasswords, new DeleteAllPasswordsCommand(vault) }
        };
        
        string command;
        do
        {
            Console.WriteLine("Enter a command:");
            Console.WriteLine(CommandFlags.CreatePassword + ". Create password");
            Console.WriteLine(CommandFlags.CreatePasswordGenerated + ". Generate and add password");
            Console.WriteLine(CommandFlags.GetPassword + ". Get password");
            Console.WriteLine(CommandFlags.GetAllPasswords + ". Get all password");
            Console.WriteLine(CommandFlags.UpdatePassword + ". Update password");
            Console.WriteLine(CommandFlags.DeletePassword + ". Delete Password");
            Console.WriteLine(CommandFlags.UpdatePasswordGenerated + ". Update password details with generated password");
            Console.WriteLine(CommandFlags.SearchPasswordEntries + ". Search password entries");
            Console.WriteLine(CommandFlags.UpdateMasterPassword + ". Update Master Password");
            Console.WriteLine(CommandFlags.Exit + ". Exit");
            Console.WriteLine(CommandFlags.DeleteAllPasswords + ". Delete all Passwords");
            Console.Write("Enter your choice: ");
            command = Console.ReadLine()?.ToLower();

            if (commands.TryGetValue(command, out var selectedCommand))
            {
                selectedCommand.Execute();
            }
            else
            {
                Console.WriteLine("Invalid command");
            }
        } while (command != "0");
    }
}