using KeyVaultCli.Commands;

namespace KeyVaultCli.Application;

public class CommandService
{
    private readonly Dictionary<string, ICommand> _commands;

    public CommandService(Dictionary<string, ICommand> commands)
    {
        _commands = commands;
    }

    public bool ExecuteCommand(string command)
    {
        if (_commands.TryGetValue(command, out var selectedCommand))
        {
            selectedCommand.Execute();
            return true;
        }
        else
        {
            return false;
        }
    }
}