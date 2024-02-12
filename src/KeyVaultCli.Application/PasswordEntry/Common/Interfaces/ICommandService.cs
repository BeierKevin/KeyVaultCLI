using KeyVaultCli.Application.PasswordEntry.Common.Constants;

namespace KeyVaultCli.Application.PasswordEntry.Common.Interfaces;

public interface ICommandService
{
    bool ExecuteCommand(CommandFlag command, out string errorMessage);
    string? GetCommandValidationErrorMessage(string commandString);
    public bool IsCommandSupported(string commandString);
    public bool IsCommandRecognized(string commandString);
    bool IsExitCommand(string commandString);
}