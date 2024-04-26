using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Application.PasswordEntry.Commands.CreatePasswordEntry
{
    public class CreatePasswordGenerateCommand(IVault vault, IConsoleService consoleService) : ICommand
    {
        private readonly IVault vault = vault ?? throw new ArgumentNullException(nameof(vault));

        private readonly IConsoleService consoleService =
            consoleService ?? throw new ArgumentNullException(nameof(consoleService));

        private readonly string serviceNamePrompt = "Enter service name for the new password: ";
        private readonly string accountNamePrompt = "Enter account name for the new password: ";
        private readonly string passwordLengthPrompt = "Enter the desired password length: ";
        private readonly string urlPrompt = "Enter the URL (leave empty if not applicable): ";
        private readonly string categoryPrompt = "Enter the category (leave empty if not applicable): ";

        private readonly string invalidLengthError =
            "Invalid input for password length. Ensure you enter a valid number.";

        private readonly string successMessage =
            "A new password has been created and stored for {0}, {1} with the value {2}.";

        public void Execute()
        {
            try
            {
                var serviceName = consoleService.GetInputFromPrompt(serviceNamePrompt);
                if (string.IsNullOrWhiteSpace(serviceName))
                {
                    consoleService.WriteError("Service name must not be empty.");
                    return;
                }

                var accountName = consoleService.GetInputFromPrompt(accountNamePrompt);
                if (string.IsNullOrWhiteSpace(accountName))
                {
                    consoleService.WriteError("Account name must not be empty.");
                    return;
                }

                var passwordLength = GetPasswordLength();
                if (passwordLength < 1)
                {
                    consoleService.WriteError("Password length must be greater than 0.");
                    return;
                }

                var url = consoleService.GetInputFromPrompt(urlPrompt);
                var category = consoleService.GetInputFromPrompt(categoryPrompt);

                var password =
                    vault.GenerateAndAddPasswordEntry(serviceName, accountName, passwordLength, url, category);
                if (string.IsNullOrEmpty(password))
                {
                    consoleService.WriteError("The password was not generated. Please retry the operation.");
                    return;
                }

                consoleService.WriteSuccess(string.Format(successMessage, serviceName, accountName, password));
            }
            catch (Exception ex)
            {
                // Log or display the precise error message
                consoleService.WriteError("An error occurred while trying to generate a password. Details: " +
                                          ex.Message);
            }
        }

        private int GetPasswordLength()
        {
            var passwordLengthStr = consoleService.GetInputFromPrompt(passwordLengthPrompt);
            if (!int.TryParse(passwordLengthStr, out var passwordLength) || passwordLength < 1)
            {
                consoleService.WriteError(invalidLengthError);
                return -1;
            }

            return passwordLength;
        }
    }
}