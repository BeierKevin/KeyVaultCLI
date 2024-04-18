using KeyVaultCli.Application.Common.Interfaces;

namespace KeyVaultCli.Domain.UnitTests.Fakes;

public class FakeConsoleService : IConsoleService
{
    public string GetInputFromPrompt(string prompt) { return string.Empty; }
    public void WriteText(string message) { /* do nothing */ }
    public void WriteInfo(string message) { /* do nothing */ }
    public void WriteSuccess(string message) { /* do nothing */ }
    public void WriteWarning(string message) { /* do nothing */ }
    public void WriteError(string message) { /* do nothing */ }
    public void WriteTable(string[] headers, List<List<object>> data) { /* do nothing */ }
    public void Clear() { /* do nothing */ }
}