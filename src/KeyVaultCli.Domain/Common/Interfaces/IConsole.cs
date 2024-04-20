namespace KeyVaultCli.Domain.Common.Interfaces;

public interface IConsole
{
    string GetInputFromPrompt(string prompt, bool isBold = false);
    bool GetUserConfirmation(string promptMessage, bool isBold = false);
    void WriteText(string message, bool isBold = false);
    void WriteInfo(string message, bool isBold = false);
    void WriteSuccess(string message, bool isBold = false);
    void WriteWarning(string message, bool isBold = false);
    void WriteError(string message, bool isBold = false);
    void WriteTable(string[] headers, List<List<object>> data);
    void Clear();
}