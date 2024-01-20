using KeyVaultCli.Core;

namespace KeyVaultCli.Commands;

public class DeleteAllPasswordsCommand : ICommand
{
    private readonly Vault _vault;

    public DeleteAllPasswordsCommand(Vault vault)
    {
        _vault = vault;
    }

    public void Execute()
    {
        _vault.DeleteAllPasswordEntries();
        Console.WriteLine("All password entries have been deleted.");
    }
}