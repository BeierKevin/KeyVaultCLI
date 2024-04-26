using KeyVaultCli.Application.Common.Interfaces;

namespace KeyVaultCli.Domain.UnitTests.Fakes;

public class FakeConsoleService : IConsoleService
{
    public string GetInputFromPrompt(string prompt, bool isBold = false)
    {
        return string.Empty;
    }

    public bool GetUserConfirmation(string promptMessage, bool isBold = false)
    {
        return true;
    }

    public void WriteText(string message, bool isBold = false)
    {
        /* do nothing */
    }

    public void WriteInfo(string message, bool isBold = false)
    {
        /* do nothing */
    }

    public void WriteSuccess(string message, bool isBold = false)
    {
        /* do nothing */
    }

    public void WriteWarning(string message, bool isBold = false)
    {
        /* do nothing */
    }

    public void WriteError(string message, bool isBold = false)
    {
        /* do nothing */
    }

    public void WriteTable(string[] headers, List<List<object>> data)
    {
        /* do nothing */
    }

    public void Clear()
    {
        /* do nothing */
    }
}