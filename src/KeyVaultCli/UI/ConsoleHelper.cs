namespace CLI.UI;

public static class ConsoleHelper
{
    public static void WriteText(string message)
    {
        Console.WriteLine(message);
        Console.ResetColor();
    }
    // Method that formats the message as success and writes to the console
    public static void WriteSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    // Method that formats the message as error and writes to the console
    public static void WriteError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }
    
    public static void WriteInfo(string message)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    // Method that prints a table to the console
    public static void WriteTable(string[] headers, List<List<object>> data)
    {
        var table = new ConsoleTable(headers);

        foreach (var row in data)
        {
            table.AddRow(row.ToArray());
        }
    
        table.Write(Format.Default);
    }

    // Method to clear the console
    public static void Clear()
    {
        Console.Clear();
    }
  
    // Method for clean console input
    public static string GetInput(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }
}