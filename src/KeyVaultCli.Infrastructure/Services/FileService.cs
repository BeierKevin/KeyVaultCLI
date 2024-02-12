using KeyVaultCli.Application;
using KeyVaultCli.Application.PasswordEntry.Common.Interfaces;

namespace KeyVaultCli.Infrastructure;

public class FileService : IFileService
{
    public bool Exists(string path)
    {
        return File.Exists(path);
    }

    public void WriteAllText(string path, string content)
    {
        File.WriteAllText(path, content);
    }

    public string ReadAllText(string path)
    {
        return File.ReadAllText(path);
    }
}