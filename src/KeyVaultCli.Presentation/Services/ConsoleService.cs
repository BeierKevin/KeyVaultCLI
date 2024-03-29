﻿using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Presentation.UserInterface.Table;

namespace KeyVaultCli.Presentation.Services;

public class ConsoleService : IConsoleService
{
    public string GetInputFromPrompt(string prompt)
    {
        Console.Write(prompt);
        var input = Console.ReadLine();
        return input ?? string.Empty;
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