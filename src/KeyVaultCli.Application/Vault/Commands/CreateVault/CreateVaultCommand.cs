using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Application.Vault.Commands.CreateVault;

public class CreateVaultCommand(IVaultFactory vaultFactory, IConsole consoleService) : ICommand
{
    private readonly string passwordPrompt = "Enter your master password: ";

    public void Execute()
    {
        var masterPassword = consoleService.GetInputFromPrompt(passwordPrompt, true);
        var vault = vaultFactory.CreateVault(masterPassword);

        if(vault == null)
        {
            Environment.Exit(-1);
        }
    }
}