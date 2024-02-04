using KeyVaultCli.Application;
using KeyVaultCli.Infrastructure.UI;

namespace KeyVaultCli.Infrastructure;

public class ConsoleService : IConsoleService
{
    public string GetInput(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }

    public void WriteText(string message)
    {
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public void WriteInfo(string message)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public void WriteSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public void WriteError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
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