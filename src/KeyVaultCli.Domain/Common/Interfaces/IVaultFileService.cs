namespace KeyVaultCli.Domain.Common.Interfaces;

public interface IVaultFileService
{
    bool Exists(string path);
    void WriteAllText(string path, string content);
    string ReadAllText(string path);
    bool Delete(string path);
}