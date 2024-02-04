namespace KeyVaultCli.Application;

public interface ICommandService
{
    bool ExecuteCommand(string command);
}