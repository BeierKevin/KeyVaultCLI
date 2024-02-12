namespace KeyVaultCli.Application.PasswordEntry.Common.Interfaces;

public interface IFileService
{
    bool Exists(string path);
    void WriteAllText(string path, string content);
    string ReadAllText(string path);
}