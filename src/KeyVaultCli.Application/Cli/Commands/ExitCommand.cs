using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain;
using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Application.Cli.Commands;

public class ExitCommand(IConsole consoleService) : ICommand
{
    string asciiExitingApplication = "  _____      _ _   _                  _                _ _           _   _             \n | ____|_  _(_) |_(_)_ __   __ _     / \\   _ __  _ __ | (_) ___ __ _| |_(_) ___  _ __  \n |  _| \\ \\/ / | __| | '_ \\ / _` |   / _ \\ | '_ \\| '_ \\| | |/ __/ _` | __| |/ _ \\| '_ \\ \n | |___ >  <| | |_| | | | | (_| |  / ___ \\| |_) | |_) | | | (_| (_| | |_| | (_) | | | |\n |_____/_/\\_\\_|\\__|_|_| |_|\\__, | /_/   \\_\\ .__/| .__/|_|_|\\___\\__,_|\\__|_|\\___/|_| |_|\n                           |___/          |_|   |_|                                    ";
    
    public void Execute()
    {
        consoleService.WriteError(asciiExitingApplication);
    }
}