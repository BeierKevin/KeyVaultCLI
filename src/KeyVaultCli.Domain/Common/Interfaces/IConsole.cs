namespace KeyVaultCli.Domain.Common.Interfaces;

public interface IConsole
{
    string GetInputFromPrompt(string prompt);
    bool GetUserConfirmation(string promptMessage);
    void WriteText(string message);
    void WriteInfo(string message);
    void WriteSuccess(string message);
    void WriteWarning(string message);
    void WriteError(string message);
    void WriteTable(string[] headers, List<List<object>> data);
    void Clear();
}