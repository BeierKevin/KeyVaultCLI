using KeyVaultCli.Application.Common.Interfaces;
using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Application.Vault.Commands.CreateVault;

public class CreateVaultCommand(IVaultFactory vaultFactory, IConsole consoleService) : ICommand
{
    public void Execute()
    {
        consoleService.WriteInfo("Enter your master password: ");
        var masterPassword = Console.ReadLine();
        var vault = vaultFactory.CreateVault(masterPassword!);
        
        if(vault == null)
        {
            Environment.Exit(-1);
        }
    }
}