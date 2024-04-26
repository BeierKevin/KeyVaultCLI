using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Presentation.UserInterface;
using KeyVaultCli.Presentation.UserInterface.Table;

namespace KeyVaultCli.Presentation.Services
{
    public class ConsoleService : IConsoleService
    {
        ColorGenerated colors = new ColorGenerated();

        public string GetInputFromPrompt(string prompt, bool isBold = false)
        {
            if (string.IsNullOrWhiteSpace(prompt))
                throw new ArgumentException("Prompt message must not be empty.", nameof(prompt));

            WriteText(prompt, isBold);
            var input = Console.ReadLine();
            return input ?? string.Empty;
        }

        public bool GetUserConfirmation(string promptMessage, bool isBold = false)
        {
            if (string.IsNullOrWhiteSpace(promptMessage))
                throw new ArgumentException("Prompt message must not be empty.", nameof(promptMessage));

            while (true)
            {
                var input = GetInputFromPrompt(promptMessage + " (y/n): ", isBold);

                if (input.Equals("y", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }

                if (input.Equals("n", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }

                WriteInfo("Invalid input. Please enter 'y' or 'n'.", isBold);
            }
        }

        public void WriteText(string message, bool isBold = false)
        {
            var regularText = new Text(message, colors.White, isBold);
            regularText.DisplayText();
        }

        public void WriteInfo(string message, bool isBold = false)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Message must not be empty.", nameof(message));

            var infoText = new Text(message, colors.Info, isBold);
            infoText.DisplayText();
        }

        public void WriteSuccess(string message, bool isBold = false)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Message must not be empty.", nameof(message));

            var successText = new Text(message, colors.Success, isBold);
            successText.DisplayText();
        }

        public void WriteWarning(string message, bool isBold = false)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Message must not be empty.", nameof(message));

            var warningText = new Text(message, colors.Warning, isBold);
            warningText.DisplayText();
        }

        public void WriteError(string message, bool isBold = false)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Message must not be empty.", nameof(message));

            var errorText = new Text(message, colors.Error, isBold);
            errorText.DisplayText();
        }

        public void WriteTable(string[] headers, List<List<object>> data)
        {
            if (headers == null || headers.Length == 0)
                throw new ArgumentException("Headers cannot be null or empty.", nameof(headers));

            if (data == null)
                throw new ArgumentNullException(nameof(data), "Data cannot be null.");

            var table = new ConsoleTable(headers);

            foreach (var row in data)
            {
                if (row == null)
                    throw new ArgumentException("Row data cannot be null.", nameof(row));

                table.AddRow(row.ToArray());
            }

            table.Write();
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}