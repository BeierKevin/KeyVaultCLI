namespace KeyVaultCli.Domain.Common.Interfaces;

public interface ICompromisedPasswordService
{
    Task<bool> HasBeenCompromisedAsync(string password);
}