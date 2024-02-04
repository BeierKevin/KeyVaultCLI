namespace KeyVaultCli.Application;

public interface IFileService
{
    bool Exists(string path);
    void WriteAllText(string path, string content);
    string ReadAllText(string path);
}