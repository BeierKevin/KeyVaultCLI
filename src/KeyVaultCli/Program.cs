using CLI.Commands;
using CLI.UI;

namespace CLI;

class Program
{
    private static void Main(string[] args)
    {
        Console.Title = "KeyVaultCli";
        ConsoleHelper.WriteInfo("Welcome to KeyVault");
        ConsoleHelper.WriteInfo("Enter your master password: ");
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
            ConsoleHelper.WriteInfo("Welcome to KeyVault");
            ConsoleHelper.WriteText("Enter a command:");
            ConsoleHelper.WriteInfo(CommandFlags.CreatePassword + ". Create password");
            ConsoleHelper.WriteInfo(CommandFlags.CreatePasswordGenerated + ". Generate and add password");
            ConsoleHelper.WriteInfo(CommandFlags.GetPassword + ". Get password");
            ConsoleHelper.WriteInfo(CommandFlags.GetAllPasswords + ". Get all password");
            ConsoleHelper.WriteInfo(CommandFlags.UpdatePassword + ". Update password");
            ConsoleHelper.WriteInfo(CommandFlags.DeletePassword + ". Delete Password");
            ConsoleHelper.WriteInfo(CommandFlags.UpdatePasswordGenerated + ". Update password details with generated password");
            ConsoleHelper.WriteInfo(CommandFlags.SearchPasswordEntries + ". Search password entries");
            ConsoleHelper.WriteInfo(CommandFlags.UpdateMasterPassword + ". Update Master Password");
            ConsoleHelper.WriteInfo(CommandFlags.Exit + ". Exit");
            ConsoleHelper.WriteInfo(CommandFlags.DeleteAllPasswords + ". Delete all Passwords");
            ConsoleHelper.WriteText("Enter your choice: ");
            command = Console.ReadLine()?.Trim().ToLower();

            if (commands.TryGetValue(command, out var selectedCommand))
            {
                selectedCommand.Execute();
            }
            else
            {
                ConsoleHelper.WriteError("Invalid command");
            }
        } while (command != "0");
    }
}