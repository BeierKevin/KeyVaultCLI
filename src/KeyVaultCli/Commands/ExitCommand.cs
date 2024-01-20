using CLI.UI;

namespace CLI.Commands;

public class ExitCommand : ICommand
{
    public void Execute()
    {
        ConsoleHelper.WriteInfo("Exiting application");
    }
}