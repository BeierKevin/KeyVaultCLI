# Entwurfsmuster

## Command

Das Command-Muster (englisch: Command Pattern) ist ein Verhaltensmuster (Behavioral Design Pattern) und wird genutzt, um Anfragen oder einfache Operationen in Form eines Objekts zu kapseln.

````csharp
using KeyVaultCli.Application.Common.Constants;

namespace KeyVaultCli.Application.Common.Interfaces;

public interface ICommandService
{
    bool ExecuteCommand(CommandFlag command, out string errorMessage);
    string? GetCommandValidationErrorMessage(string commandString);
    public bool IsCommandSupported(string commandString);
    public bool IsCommandRecognized(string commandString);
    bool IsExitCommand(string commandString);
}
````

````csharp
using KeyVaultCli.Application.Common.Constants;
using KeyVaultCli.Application.Common.Interfaces;

namespace KeyVaultCli.Infrastructure.Services;

public class CommandService(Dictionary<CommandFlag, ICommand> commands) : ICommandService
{
    public bool ExecuteCommand(CommandFlag command, out string errorMessage)
    {
        errorMessage = string.Empty;

        if (commands.TryGetValue(command, out var selectedCommand))
        {
            try
            {
                selectedCommand.Execute();
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
        else
        {
            errorMessage = "Invalid command";
        }

        return false;
    }

    public bool IsCommandSupported(string commandString)
    {
        return Enum.TryParse<CommandFlag>(commandString, true, out var commandFlag) && commands.ContainsKey(commandFlag);
    }

    public bool IsCommandRecognized(string commandString)
    {
        return Enum.TryParse<CommandFlag>(commandString, true, out var _);
    }

    public string? GetCommandValidationErrorMessage(string commandString)
    {
        if (!IsCommandRecognized(commandString))
        {
            return "Command not recognized. Please try again.";
        }

        return !IsCommandSupported(commandString) ? "Command recognized but not supported. Please try again." : null;
    }
    
    public bool IsExitCommand(string commandString)
    {
        return Enum.TryParse<CommandFlag>(commandString, true, out var commandFlag) && commandFlag == CommandFlag.Exit;
    }
}
````

## Factory

Das Factory-Entwurfsmuster wird verwendet, um die Erstellung von Objekten zu kapseln und zu zentralisieren. In dem gezeigten Beispiel wird die `VaultFactory`-Klasse verwendet, um Instanzen der `Vault`-Klasse zu erstellen, die eine zentrale Rolle im Key Vault CLI-Projekt spielen. Die `VaultFactory` bietet Methoden wie `CreateVault`, um eine neue Tresorinstanz zu erstellen, und `DeleteVault`, um einen vorhandenen Tresor zu löschen. Durch die Verwendung der Factory kann die Erstellung und Konfiguration von `Vault`-Instanzen zentralisiert und die Code-Wiederholung reduziert werden.

````csharp
using KeyVaultCli.Domain.Common.Interfaces;
using KeyVaultCli.Domain.Entities;

namespace KeyVaultCli.Domain.Factories;

// Factory Pattern
public class VaultFactory(
    IConsole vaultConsoleService,
    IVaultEncryptionService vaultEncryptionService,
    IVaultFileService vaultFileService,
    IVaultPasswordGenerator vaultPasswordGenerator)
    : IVaultFactory
{
    private const string VaultFilePath = "vault.dat";
    private const string MasterPasswordFilePath = "masterpassword.dat";
    private IVault _vault;

    public IVault? CreateVault(string masterPassword)
    {
        if (string.IsNullOrEmpty(masterPassword))
        {
            vaultConsoleService.WriteError("Master password should not be empty");
            return null;
        }

        _vault = new Vault(VaultFilePath, MasterPasswordFilePath, masterPassword, vaultEncryptionService, vaultFileService, vaultPasswordGenerator);
        var savedPassword = _vault.LoadMasterPassword();
        if (savedPassword == null)
        {
            _vault.SaveMasterPassword();
        }
        else if (savedPassword != masterPassword)
        {
            vaultConsoleService.WriteError("Invalid master password. Exit.");
            return null;
        }

        return _vault;
    }

    public bool DeleteVault()
    {
        var isVaultDeleted = vaultFileService.Delete(VaultFilePath);
        var isMasterPasswordDeleted = vaultFileService.Delete(MasterPasswordFilePath);
        var deleted = isVaultDeleted && isMasterPasswordDeleted;
        return deleted;
    }
    
    public IVault GetVault()
    {
        return _vault;
    }
}
````

### UML-Diagramm

[//]: # (TODO: Add UML diagram for Factory pattern)

## Builder

Das Builder-Entwurfsmuster wird verwendet, um die Erstellung von komplexen Objekten zu vereinfachen und zu strukturieren. Im vorliegenden Beispiel wird die `PasswordEntryBuilder`-Klasse verwendet, um Instanzen der `PasswordEntry`-Klasse zu erstellen, die die Daten für Passworteinträge im Key Vault CLI-Projekt speichern. Der Builder bietet Methoden wie `SetServiceName`, `SetAccountName` und `SetEncryptedPassword`, um die verschiedenen Eigenschaften des Passworteintrags festzulegen, und eine `Build`-Methode, um den finalen Passworteintrag zu erstellen. Durch die Verwendung des Builders können Passworteinträge schrittweise erstellt und konfiguriert werden, was die Lesbarkeit und Wartbarkeit des Codes verbessert.

````csharp
using KeyVaultCli.Domain.Entities;

namespace KeyVaultCli.Domain.Factories;

// Builder Pattern
public class PasswordEntryBuilder
{
    private readonly PasswordEntry _passwordEntry = new()
    {
        EntryId = Guid.NewGuid(),
        CreationDate = DateTime.UtcNow,
        LastModifiedDate = DateTime.UtcNow
    };

    public PasswordEntryBuilder SetServiceName(string serviceName)
    {
        _passwordEntry.ServiceName = serviceName;
        _passwordEntry.LastModifiedDate = DateTime.UtcNow;
        return this;
    }

    public PasswordEntryBuilder SetAccountName(string accountName)
    {
        _passwordEntry.AccountName = accountName;
        _passwordEntry.LastModifiedDate = DateTime.UtcNow;
        return this;
    }

    public PasswordEntryBuilder SetEncryptedPassword(string encryptedPassword)
    {
        _passwordEntry.EncryptedPassword = encryptedPassword;
        _passwordEntry.LastModifiedDate = DateTime.UtcNow;
        return this;
    }

    public PasswordEntry Build()
    {
        return _passwordEntry;
    }
}
````

### UML-Diagramm

[//]: # (TODO: Add UML diagram for Builder pattern)
