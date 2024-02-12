using KeyVaultCli.Application.Cli.Commands;
using KeyVaultCli.Application.Common.Constants;
using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Application.PasswordEntry.Commands.CreatePasswordEntry;
using KeyVaultCli.Application.PasswordEntry.Commands.DeletePasswordEntry;
using KeyVaultCli.Application.PasswordEntry.Commands.GetPasswordEntry;
using KeyVaultCli.Application.PasswordEntry.Commands.UpdatePasswordEntry;
using KeyVaultCli.Domain.Common.Interfaces;
using KeyVaultCli.Domain.Factories;
using KeyVaultCli.Infrastructure.Cryptography;
using KeyVaultCli.Infrastructure.Services;

namespace KeyVaultCli.Presentation;

internal abstract class Program
{
    private static void Main()
    {
        IConsoleService consoleService = new ConsoleService();
        IEncryptionService encryptionService = new EncryptionService();
        IFileService fileService = new FileService();
        IPasswordGenerator passwordGenerator = new PasswordGenerator();
        IVaultFactory vaultFactory = new VaultFactory(consoleService, encryptionService, fileService, passwordGenerator);
        
        Console.Title = "KeyVaultCli";
        consoleService.WriteInfo("Welcome to KeyVault");
        consoleService.WriteInfo("Enter your master password: ");
        var masterPassword = Console.ReadLine();
        var vault = vaultFactory.CreateVault(masterPassword!);

        if(vault == null) return;
        
        // Command Pattern
        var commands = new Dictionary<CommandFlag, ICommand>
        {
            { CommandFlag.CreatePassword, new CreatePasswordCommand(vault, consoleService) },
            { CommandFlag.CreatePasswordGenerated, new CreatePasswordGenerateCommand(vault, consoleService) },
            { CommandFlag.GetPassword, new GetPasswordCommand(vault, consoleService) },
            { CommandFlag.GetAllPasswords, new GetAllPasswordsCommand(vault, consoleService) },
            { CommandFlag.UpdatePassword, new UpdatePasswordCommand(vault, consoleService) },
            { CommandFlag.UpdatePasswordGenerated, new UpdatePasswordGeneratedCommand(vault, consoleService) },
            { CommandFlag.DeletePassword, new DeletePasswordCommand(vault, consoleService) },
            { CommandFlag.SearchPasswordEntries, new SearchPasswordEntriesCommand(vault, consoleService) },
            { CommandFlag.UpdateMasterPassword, new UpdateMasterPasswordCommand(vault, consoleService) },
            { CommandFlag.Exit, new ExitCommand(consoleService) },
            { CommandFlag.DeleteAllPasswords, new DeleteAllPasswordsCommand(vault, consoleService) }
        };
        
        var commandService = new CommandService(commands);
        
        string command;
        do
        {
            consoleService.WriteText("Enter a command:");
            consoleService.WriteInfo((int)CommandFlag.CreatePassword + ". CreatePasswordEntry password");
            consoleService.WriteInfo((int)CommandFlag.CreatePasswordGenerated + ". Generate and add password");
            consoleService.WriteInfo((int)CommandFlag.GetPassword + ". GetPasswordEntry password");
            consoleService.WriteInfo((int)CommandFlag.GetAllPasswords + ". GetPasswordEntry all password");
            consoleService.WriteInfo((int)CommandFlag.UpdatePassword + ". UpdatePasswordEntry password");
            consoleService.WriteInfo((int)CommandFlag.DeletePassword + ". DeletePasswordEntry Password");
            consoleService.WriteInfo((int)CommandFlag.UpdatePasswordGenerated + ". UpdatePasswordEntry password details with generated password");
            consoleService.WriteInfo((int)CommandFlag.SearchPasswordEntries + ". Search password entries");
            consoleService.WriteInfo((int)CommandFlag.UpdateMasterPassword + ". UpdatePasswordEntry Master Password");
            consoleService.WriteInfo((int)CommandFlag.Exit + ". Exit");
            consoleService.WriteInfo((int)CommandFlag.DeleteAllPasswords + ". DeletePasswordEntry all Passwords");
            command = consoleService.GetInput("Enter your choice: ");

            
            var validationErrorMessage = commandService.GetCommandValidationErrorMessage(command);
            if (validationErrorMessage == null)
            {
                var commandFlag = Enum.Parse<CommandFlag>(command);
                if (commandService.ExecuteCommand(commandFlag, out var executionError))
                {
                    consoleService.WriteText("------------------------");
                }
                else 
                {
                    consoleService.WriteError(executionError);
                }
            }
            else 
            {
                consoleService.WriteError(validationErrorMessage);
            }
        } while (commandService.IsExitCommand(command) == false);
    }
}