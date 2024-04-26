using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;
using System;

namespace KeyVaultCli.Application.PasswordEntry.Commands.DeletePasswordEntry
{
    public class DeleteAllPasswordsCommand(IVault vault, IConsoleService consoleService) : ICommand
    {
        private readonly IVault vault = vault ?? throw new ArgumentNullException(nameof(vault));

        private readonly IConsoleService consoleService =
            consoleService ?? throw new ArgumentNullException(nameof(consoleService));

        private readonly string confirmationPrompt = "Are you sure you want to delete all passwords?";
        private readonly string successMessage = "All passwords have been deleted.";
        private readonly string errorMessage = "Operation cancelled.";

        public void Execute()
        {
            try
            {
                var confirmation = consoleService.GetUserConfirmation(confirmationPrompt);
                if (confirmation)
                {
                    vault.DeleteAllPasswordEntries();
                    consoleService.WriteSuccess(successMessage);
                }
                else
                {
                    consoleService.WriteError(errorMessage);
                }
            }
            catch (Exception ex)
            {
                consoleService.WriteError("An error occurred: " + ex.Message);
            }
        }
    }
}