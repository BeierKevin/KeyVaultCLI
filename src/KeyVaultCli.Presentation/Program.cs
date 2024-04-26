using KeyVaultCli.Application.Cli.Commands;
using KeyVaultCli.Application.Common.Constants;
using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Application.PasswordEntry.Commands.CreatePasswordEntry;
using KeyVaultCli.Application.PasswordEntry.Commands.DeletePasswordEntry;
using KeyVaultCli.Application.PasswordEntry.Commands.GetPasswordEntry;
using KeyVaultCli.Application.PasswordEntry.Commands.UpdatePasswordEntry;
using KeyVaultCli.Application.Services;
using KeyVaultCli.Application.Vault.Commands.BackupVault;
using KeyVaultCli.Application.Vault.Commands.CreateVault;
using KeyVaultCli.Application.Vault.Commands.DeleteVault;
using KeyVaultCli.Application.Vault.Commands.RestoreVault;
using KeyVaultCli.Domain.Common.Interfaces;
using KeyVaultCli.Infrastructure.Services;
using KeyVaultCli.Presentation.Services;
using KeyVaultCli.Presentation.UserInterface;

namespace KeyVaultCli.Presentation;

internal abstract class Program
{
    private static void Main()
    {
        IConsoleService vaultConsoleService = new ConsoleService();
        IEncryptionService vaultEncryptionService = new EncryptionService();
        IFileService vaultFileService = new FileService();
        IPasswordGeneratorService vaultPasswordGenerator = new PasswordGeneratorService();
        IVaultService vaultFactory = new VaultService(vaultConsoleService, vaultEncryptionService, vaultFileService,
            vaultPasswordGenerator);

        ICommandService createVaultCommand = InitializeCommandService(vaultFactory, vaultConsoleService);

        PrintLogo(vaultConsoleService);

        createVaultCommand.ExecuteCommand(CommandFlag.CreateVault, out var error);

        var vault = vaultFactory.GetVault();

        var listOfCommands = RegisterCommands(vaultConsoleService, vaultFactory, vault);
        ICommandService commandService = new CommandService(listOfCommands);

        ProcessInputCommands(vaultConsoleService, commandService);
    }

    private static ICommandService InitializeCommandService(IVaultFactory vaultFactory, IConsoleService consoleService)
    {
        return new CommandService(new Dictionary<CommandFlag, ICommand>
        {
            { CommandFlag.CreateVault, new CreateVaultCommand(vaultFactory, consoleService) }
        });
    }

    private static void PrintLogo(IConsoleService vaultConsoleService)
    {
        Console.Title = "KeyVaultCli";
        vaultConsoleService.WriteInfo(Logo.asciiStandard, true);
    }

    private static Dictionary<CommandFlag, ICommand> RegisterCommands(IConsoleService vaultConsoleService,
        IVaultFactory vaultFactory, IVault vault)
    {
        return new Dictionary<CommandFlag, ICommand>
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
            { CommandFlag.BackupVault, new BackupVaultCommand(vault, vaultConsoleService) },
            { CommandFlag.RestoreVault, new RestoreVaultCommand(vault, vaultConsoleService) },
            { CommandFlag.DeleteAllPasswords, new DeleteAllPasswordsCommand(vault, vaultConsoleService) },
            { CommandFlag.DeleteVault, new DeleteVaultCommand(vaultFactory, vaultConsoleService) }
        };
    }

    private static void ProcessInputCommands(IConsoleService vaultConsoleService, ICommandService commandService)
    {
        string command;
        do
        {
            command = PrintPrompt(vaultConsoleService);

            ExecuteCommand(vaultConsoleService, commandService, command);
        } while (!IsExitCommand(commandService, command));
    }

    private static string PrintPrompt(IConsoleService vaultConsoleService)
    {
        vaultConsoleService.WriteText("Enter a command:", true);

        // General commands
        vaultConsoleService.WriteInfo("---- General commands ----", true);
        vaultConsoleService.WriteInfo((int)CommandFlag.Exit + ". Exit");

        // Password related commands
        vaultConsoleService.WriteInfo("---- Password related commands ----", true);
        vaultConsoleService.WriteInfo((int)CommandFlag.CreatePassword + ". Create password entry");
        vaultConsoleService.WriteInfo((int)CommandFlag.CreatePasswordGenerated +
                                      ". Create password entry with generated password");
        vaultConsoleService.WriteInfo((int)CommandFlag.GetPassword + ". Get password entry by name");
        vaultConsoleService.WriteInfo((int)CommandFlag.GetAllPasswords + ". Get all password entries");
        vaultConsoleService.WriteInfo((int)CommandFlag.UpdatePassword + ". Update password entry");
        vaultConsoleService.WriteInfo((int)CommandFlag.UpdatePasswordGenerated +
                                      ". Update password with generated password");
        vaultConsoleService.WriteInfo((int)CommandFlag.DeletePassword + ". Delete password entry");
        vaultConsoleService.WriteInfo((int)CommandFlag.DeleteAllPasswords + ". Delete all passwords in vault");
        vaultConsoleService.WriteInfo((int)CommandFlag.SearchPasswordEntries + ". Search password entries");

        // Vault related commands
        vaultConsoleService.WriteInfo("---- Vault related commands ----", true);
        vaultConsoleService.WriteInfo((int)CommandFlag.BackupVault + ". Backup vault / Export Vault");
        vaultConsoleService.WriteInfo((int)CommandFlag.RestoreVault + ". Restore vault / Import Vault");
        vaultConsoleService.WriteInfo((int)CommandFlag.DeleteVault + ". Delete Vault");

        // Master password
        vaultConsoleService.WriteInfo("---- Master password ----", true);
        vaultConsoleService.WriteInfo((int)CommandFlag.UpdateMasterPassword + ". Update Master Password");

        return vaultConsoleService.GetInputFromPrompt("Enter your choice: ", true);
    }

    private static void ExecuteCommand(IConsoleService vaultConsoleService, ICommandService commandService,
        string command)
    {
        var validationErrorMessage = commandService.GetCommandValidationErrorMessage(command);
        if (validationErrorMessage == null)
        {
            var commandFlag = (CommandFlag)Enum.Parse(typeof(CommandFlag), command);
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
    }

    private static bool IsExitCommand(ICommandService commandService, string command)
    {
        return commandService.IsExitCommand(command);
    }
}