using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Presentation.UserInterface;
using KeyVaultCli.Presentation.UserInterface.Table;

namespace KeyVaultCli.Presentation.Services;

public class ConsoleService : IConsoleService
{
    ColorGenerated colors = new ColorGenerated();
    
    public string GetInputFromPrompt(string prompt)
    {
        WriteText(prompt);
        var input = Console.ReadLine();
        return input ?? string.Empty;
    }
    
    public void WriteText(string message)
    {
        var regularText = new Text(message, colors.White);
        regularText.DisplayText();
    }

    public void WriteInfo(string message)
    {
        var infoText = new Text(message, colors.Info);
        infoText.DisplayText();
    }

    public void WriteSuccess(string message)
    {
        var successText = new Text(message, colors.Success);
        successText.DisplayText();
    }

    public void WriteWarning(string message)
    {
        var warningText = new Text(message, colors.Warning);
        warningText.DisplayText();
    }

    public void WriteError(string message)
    {
        var errorText = new Text(message, colors.Error);
        errorText.DisplayText();
    }
    
    public void WriteTable(string[] headers, List<List<object>> data)
    {
        var table = new ConsoleTable(headers);

        foreach (var row in data)
        {
            table.AddRow(row.ToArray());
        }
    
        table.Write();
    }

    public void Clear()
    {
        Console.Clear();
    }
}