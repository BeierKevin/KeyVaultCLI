using KeyVaultCli.Application;
using KeyVaultCli.Domain.Entities;

namespace KeyVaultCli.Domain.Commands;

public class DeleteAllPasswordsCommand : ICommand
{
    private readonly Vault _vault;
    private readonly IConsoleService _consoleService;

    public DeleteAllPasswordsCommand(Vault vault, IConsoleService consoleService)
    {
        _vault = vault;
        _consoleService = consoleService;
    }

    public void Execute()
    {
        var confirmation = _consoleService.GetInput("Are you sure you want to delete all passwords? (yes/no): ");
        if (confirmation.ToLower() == "yes")
        {
            _vault.DeleteAllPasswordEntries();
            _consoleService.WriteSuccess("All passwords have been deleted.");
        }
        else
        {
            _consoleService.WriteError("Operation cancelled.");
        }
    }
}