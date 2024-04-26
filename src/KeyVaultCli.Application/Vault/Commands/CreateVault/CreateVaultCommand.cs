using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;
using System;

namespace KeyVaultCli.Application.Vault.Commands.CreateVault
{
    public class CreateVaultCommand(IVaultFactory vaultFactory, IConsoleService consoleService) : ICommand
    {
        private readonly IVaultFactory vaultFactory = vaultFactory ?? throw new ArgumentNullException(nameof(vaultFactory));
        private readonly IConsoleService consoleService = consoleService ?? throw new ArgumentNullException(nameof(consoleService));

        private readonly string passwordPrompt = "Enter your master password: ";
        private readonly string vaultCreationFailureMsg = "Failed to create the vault. Please restart the application.";

        public void Execute()
        {
            try
            {
                var masterPassword = consoleService.GetInputFromPrompt(passwordPrompt, true);
                var vault = vaultFactory.CreateVault(masterPassword);

                if (vault == null)
                {
                    consoleService.WriteError(vaultCreationFailureMsg);
                    Environment.Exit(-1);
                }
            }
            catch (Exception ex)
            {
                consoleService.WriteError($"An error occurred during execute. Details: {ex.Message}");
            }
        }
    }
}