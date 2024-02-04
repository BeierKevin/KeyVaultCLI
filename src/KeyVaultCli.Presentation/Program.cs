using KeyVaultCli.Application;
using KeyVaultCli.Domain;
using KeyVaultCli.Domain.Commands;
using KeyVaultCli.Infrastructure;
using KeyVaultCli.Infrastructure.Cryptography;

namespace KeyVaultCli.Presentation;

internal abstract class Program
{
    private static void Main()
    {
        IConsoleService consoleService = new ConsoleService();
        IEncryptionService encryptionService = new EncryptionService();
        IFileService fileService = new FileService();
        IVaultFactory vaultFactory = new VaultFactory(consoleService, encryptionService, fileService);
        
        Console.Title = "KeyVaultCli";
        consoleService.WriteInfo("Welcome to KeyVault");
        consoleService.WriteInfo("Enter your master password: ");
        var masterPassword = Console.ReadLine();
        var vault = vaultFactory.CreateVault(masterPassword!);

        if(vault == null) return;
        
        // Command Pattern
        var commands = new Dictionary<string, ICommand>
        {
            { CommandFlags.CreatePassword, new CreatePasswordCommand(vault, consoleService) },
            { CommandFlags.CreatePasswordGenerated, new CreatePasswordGenerateCommand(vault, consoleService) },
            { CommandFlags.GetPassword, new GetPasswordCommand(vault, consoleService) },
            { CommandFlags.GetAllPasswords, new GetAllPasswordsCommand(vault, consoleService) },
            { CommandFlags.UpdatePassword, new UpdatePasswordCommand(vault, consoleService) },
            { CommandFlags.UpdatePasswordGenerated, new UpdatePasswordGeneratedCommand(vault, consoleService) },
            { CommandFlags.DeletePassword, new DeletePasswordCommand(vault, consoleService) },
            { CommandFlags.SearchPasswordEntries, new SearchPasswordEntriesCommand(vault, consoleService) },
            { CommandFlags.UpdateMasterPassword, new UpdateMasterPasswordCommand(vault, consoleService) },
            { CommandFlags.Exit, new ExitCommand(consoleService) },
            { CommandFlags.DeleteAllPasswords, new DeleteAllPasswordsCommand(vault, consoleService) }
        };
        
        var commandService = new CommandService(commands);
        
        string command;
        do
        {
            consoleService.WriteText("Enter a command:");
            consoleService.WriteInfo(CommandFlags.CreatePassword + ". Create password");
            consoleService.WriteInfo(CommandFlags.CreatePasswordGenerated + ". Generate and add password");
            consoleService.WriteInfo(CommandFlags.GetPassword + ". Get password");
            consoleService.WriteInfo(CommandFlags.GetAllPasswords + ". Get all password");
            consoleService.WriteInfo(CommandFlags.UpdatePassword + ". Update password");
            consoleService.WriteInfo(CommandFlags.DeletePassword + ". Delete Password");
            consoleService.WriteInfo(CommandFlags.UpdatePasswordGenerated + ". Update password details with generated password");
            consoleService.WriteInfo(CommandFlags.SearchPasswordEntries + ". Search password entries");
            consoleService.WriteInfo(CommandFlags.UpdateMasterPassword + ". Update Master Password");
            consoleService.WriteInfo(CommandFlags.Exit + ". Exit");
            consoleService.WriteInfo(CommandFlags.DeleteAllPasswords + ". Delete all Passwords");
            command = consoleService.GetInput("Enter your choice: ");

            if (commandService.ExecuteCommand(command))
            {
                // Use the function in the service to execute the command.
                // Successful execution, do nothing.
                consoleService.WriteText("------------------------");
            }
            else
            {
                consoleService.WriteError("Invalid command");
            }
        } while (command != "0");
    }
}