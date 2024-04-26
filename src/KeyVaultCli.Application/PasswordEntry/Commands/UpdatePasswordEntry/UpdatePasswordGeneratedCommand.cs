using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;
using System;

namespace KeyVaultCli.Application.PasswordEntry.Commands.UpdatePasswordEntry
{
    public class UpdatePasswordGeneratedCommand(IVault vault, IConsoleService consoleService) : ICommand
    {
        private readonly IVault vault = vault ?? throw new ArgumentNullException(nameof(vault));
        private readonly IConsoleService consoleService = consoleService ?? throw new ArgumentNullException(nameof(consoleService));

        private readonly string oldServiceNamePrompt = "Enter old service name: ";
        private readonly string oldAccountNamePrompt = "Enter old account name: ";
        private readonly string newServiceNamePrompt = "Enter new service name: ";
        private readonly string newAccountNamePrompt = "Enter new account name: ";
        private readonly string newUrlPrompt = "Enter new URL: ";
        private readonly string newCategoryPrompt = "Enter new Category: ";
        private readonly string passwordLengthPrompt = "Enter the number of characters for the new password (e.g. 10): ";
        private readonly string invalidLengthError = "Invalid input for password length. Ensure you enter a valid number.";
        private readonly string successMessage = "Password entry for {0}, {1} has been updated with new details.";
        private readonly string errorMessage = "Failed to update the password entry. Ensure the old service and account names are correct.";

        public void Execute()
        {
            try
            {
                var oldServiceName = consoleService.GetInputFromPrompt(oldServiceNamePrompt);
                var oldAccountName = consoleService.GetInputFromPrompt(oldAccountNamePrompt);
                var newServiceName = consoleService.GetInputFromPrompt(newServiceNamePrompt);
                var newAccountName = consoleService.GetInputFromPrompt(newAccountNamePrompt);
                var newUrl = consoleService.GetInputFromPrompt(newUrlPrompt);
                var newCategory = consoleService.GetInputFromPrompt(newCategoryPrompt);
                var passwordLengthInput = consoleService.GetInputFromPrompt(passwordLengthPrompt);

                if (!int.TryParse(passwordLengthInput, out var passwordLength))
                {
                    consoleService.WriteError(invalidLengthError);
                    return;
                }

                if (vault.UpdatePasswordEntry(oldServiceName, oldAccountName, newServiceName, newAccountName, passwordLength, null, newUrl: newUrl, newCategory: newCategory))
                {
                    consoleService.WriteSuccess(string.Format(successMessage, oldServiceName, oldAccountName));
                }
                else
                {
                    consoleService.WriteError(errorMessage);
                }
            }
            catch (Exception ex)
            {
                consoleService.WriteError($"An error occurred while trying to update the password entry. Details: {ex.Message}");
            } 
        }
    }
}