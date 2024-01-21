namespace KeyVaultCli.Application;

public interface IConsoleService
{
    string GetInput(string prompt);
    void WriteText(string message);
    void WriteInfo(string message);
    void WriteSuccess(string message);
    void WriteError(string message);
    void WriteTable(string[] headers, List<List<object>> data);
    void Clear();
}