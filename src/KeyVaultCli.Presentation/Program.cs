﻿using KeyVaultCli.Application.Cli.Commands;
using KeyVaultCli.Application.Common.Constants;
using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Application.PasswordEntry.Commands.CreatePasswordEntry;
using KeyVaultCli.Application.PasswordEntry.Commands.DeletePasswordEntry;
using KeyVaultCli.Application.PasswordEntry.Commands.GetPasswordEntry;
using KeyVaultCli.Application.PasswordEntry.Commands.UpdatePasswordEntry;
using KeyVaultCli.Application.Services;
using KeyVaultCli.Application.Vault.Commands.CreateVault;
using KeyVaultCli.Application.Vault.Commands.DeleteVault;
using KeyVaultCli.Infrastructure.Services;
using KeyVaultCli.Presentation.Services;

namespace KeyVaultCli.Presentation;

internal abstract class Program
{
    private static void Main()
    {
        IConsoleService vaultConsoleService = new ConsoleService();
        IEncryptionService vaultEncryptionService = new EncryptionService();
        IFileService vaultFileService = new FileService();
        IPasswordGeneratorService vaultPasswordGenerator = new PasswordGeneratorService();
        IVaultService vaultFactory = new VaultService(vaultConsoleService, vaultEncryptionService, vaultFileService, vaultPasswordGenerator);
        ICommandService createVaultCommand = new CommandService(new Dictionary<CommandFlag, ICommand>
        {
            { CommandFlag.CreateVault, new CreateVaultCommand(vaultFactory, vaultConsoleService) }
        });
        
        Console.Title = "KeyVaultCli";
        vaultConsoleService.WriteInfo("Welcome to KeyVault");
        createVaultCommand.ExecuteCommand(CommandFlag.CreateVault, out var error);
        var vault = vaultFactory.GetVault();
        
        // Command Pattern
        var commands = new Dictionary<CommandFlag, ICommand>
        {
            { CommandFlag.CreatePassword, new CreatePasswordCommand(vault, vaultConsoleService) },
            { CommandFlag.CreatePasswordGenerated, new CreatePasswordGenerateCommand(vault, vaultConsoleService) },
            { CommandFlag.GetPassword, new GetPasswordCommand(vault, vaultConsoleService) },
            { CommandFlag.GetAllPasswords, new GetAllPasswordsCommand(vault, vaultConsoleService) },
            { CommandFlag.UpdatePassword, new UpdatePasswordCommand(vault, vaultConsoleService) },
            { CommandFlag.UpdatePasswordGenerated, new UpdatePasswordGeneratedCommand(vault, vaultConsoleService) },
            { CommandFlag.DeletePassword, new DeletePasswordCommand(vault, vaultConsoleService) },
            { CommandFlag.SearchPasswordEntries, new SearchPasswordEntriesCommand(vault, vaultConsoleService) },
            { CommandFlag.UpdateMasterPassword, new UpdateMasterPasswordCommand(vault, vaultConsoleService) },
            { CommandFlag.Exit, new ExitCommand(vaultConsoleService) },
            { CommandFlag.DeleteAllPasswords, new DeleteAllPasswordsCommand(vault, vaultConsoleService) },
            { CommandFlag.DeleteVault, new DeleteVaultCommand(vaultFactory, vaultConsoleService) }
        };
        
        ICommandService commandService = new CommandService(commands);
        
        string command;
        do
        {
            vaultConsoleService.WriteText("Enter a command:");
            vaultConsoleService.WriteInfo((int)CommandFlag.CreatePassword + ". Create password entry");
            vaultConsoleService.WriteInfo((int)CommandFlag.CreatePasswordGenerated + ". Create password entry with generated password");
            vaultConsoleService.WriteInfo((int)CommandFlag.GetPassword + ". Get password entry by name");
            vaultConsoleService.WriteInfo((int)CommandFlag.GetAllPasswords + ". Get all password entries");
            vaultConsoleService.WriteInfo((int)CommandFlag.UpdatePassword + ". Update password entry");
            vaultConsoleService.WriteInfo((int)CommandFlag.DeletePassword + ". Delete password entry");
            vaultConsoleService.WriteInfo((int)CommandFlag.UpdatePasswordGenerated + ". Update password with generated password");
            vaultConsoleService.WriteInfo((int)CommandFlag.SearchPasswordEntries + ". Search password entries");
            vaultConsoleService.WriteInfo((int)CommandFlag.UpdateMasterPassword + ". Update Master Password");
            vaultConsoleService.WriteInfo((int)CommandFlag.Exit + ". Exit");
            vaultConsoleService.WriteInfo((int)CommandFlag.DeleteAllPasswords + ". Delete all passwords in vault");
            vaultConsoleService.WriteInfo((int)CommandFlag.DeleteVault + ". Delete Vault");
            command = vaultConsoleService.GetInputFromPrompt("Enter your choice: ");

            var validationErrorMessage = commandService.GetCommandValidationErrorMessage(command);
            if (validationErrorMessage == null)
            {
                var commandFlag = Enum.Parse<CommandFlag>(command);
                if (commandService.ExecuteCommand(commandFlag, out var executionError))
                {
                    vaultConsoleService.WriteText("------------------------");
                }
                else 
                {
                    vaultConsoleService.WriteError(executionError);
                }
            }
            else 
            {
                vaultConsoleService.WriteError(validationErrorMessage);
            }
        } while (commandService.IsExitCommand(command) == false);
    }
}