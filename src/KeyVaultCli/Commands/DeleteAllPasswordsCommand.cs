using CLI.UI;

namespace CLI.Commands;

public class DeleteAllPasswordsCommand : ICommand
{
    private readonly Vault _vault;

    public DeleteAllPasswordsCommand(Vault vault)
    {
        _vault = vault;
    }

    public void Execute()
    {
        var confirmation = ConsoleHelper.GetInput("Are you sure you want to delete all passwords? (yes/no): ");
        if (confirmation.ToLower() == "yes")
        {
            _vault.DeleteAllPasswordEntries();
            ConsoleHelper.WriteSuccess("All passwords have been deleted.");
        }
        else
        {
            ConsoleHelper.WriteError("Operation cancelled.");
        }
    }
}