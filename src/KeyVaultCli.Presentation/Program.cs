using KeyVaultCli.Application;
using KeyVaultCli.Application.Cli;
using KeyVaultCli.Application.Cli.Commands;
using KeyVaultCli.Application.PasswordEntry.Commands.CreatePasswordEntry;
using KeyVaultCli.Application.PasswordEntry.Commands.DeletePasswordEntry;
using KeyVaultCli.Application.PasswordEntry.Commands.GetPasswordEntry;
using KeyVaultCli.Application.PasswordEntry.Commands.UpdatePasswordEntry;
using KeyVaultCli.Domain;
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
            consoleService.WriteInfo(CommandFlags.CreatePassword + ". CreatePasswordEntry password");
            consoleService.WriteInfo(CommandFlags.CreatePasswordGenerated + ". Generate and add password");
            consoleService.WriteInfo(CommandFlags.GetPassword + ". GetPasswordEntry password");
            consoleService.WriteInfo(CommandFlags.GetAllPasswords + ". GetPasswordEntry all password");
            consoleService.WriteInfo(CommandFlags.UpdatePassword + ". UpdatePasswordEntry password");
            consoleService.WriteInfo(CommandFlags.DeletePassword + ". DeletePasswordEntry Password");
            consoleService.WriteInfo(CommandFlags.UpdatePasswordGenerated + ". UpdatePasswordEntry password details with generated password");
            consoleService.WriteInfo(CommandFlags.SearchPasswordEntries + ". Search password entries");
            consoleService.WriteInfo(CommandFlags.UpdateMasterPassword + ". UpdatePasswordEntry Master Password");
            consoleService.WriteInfo(CommandFlags.Exit + ". Exit");
            consoleService.WriteInfo(CommandFlags.DeleteAllPasswords + ". DeletePasswordEntry all Passwords");
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