using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;
using System;

namespace KeyVaultCli.Application.Vault.Commands.DeleteVault
{
    public class DeleteVaultCommand(IVaultFactory vault, IConsoleService consoleService) : ICommand
    {
        private readonly IVaultFactory vault = vault ?? throw new ArgumentNullException(nameof(vault));

        private readonly IConsoleService consoleService =
            consoleService ?? throw new ArgumentNullException(nameof(consoleService));

        private readonly string confirmationPrompt = "Are you sure you want to delete the vault?";
        private readonly string deleteSuccessMsg = "Deleted Vault with all passwords in it!";
        private readonly string deleteErrMsg = "Closing application, to create a new vault run the application again.";
        private readonly string operationCancelMsg = "Operation cancelled.";

        public void Execute()
        {
            try
            {
                if (consoleService.GetUserConfirmation(confirmationPrompt))
                {
                    vault.DeleteVault();
                    consoleService.WriteSuccess(deleteSuccessMsg);
                    consoleService.WriteError(deleteErrMsg);
                    Environment.Exit(-1);
                }
                else
                {
                    consoleService.WriteError(operationCancelMsg);
                }
            }
            catch (Exception ex)
            {
                consoleService.WriteError($"An error occurred during execute. Details: {ex.Message}");
            }
        }
    }
}