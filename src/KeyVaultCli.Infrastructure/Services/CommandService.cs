using KeyVaultCli.Application.PasswordEntry.Common.Constants;
using KeyVaultCli.Application.PasswordEntry.Common.Interfaces;
using KeyVaultCli.Domain;

namespace KeyVaultCli.Application;

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