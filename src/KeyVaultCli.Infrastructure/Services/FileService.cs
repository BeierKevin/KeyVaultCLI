using KeyVaultCli.Application.Common.Interfaces;

namespace KeyVaultCli.Infrastructure.Services;

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

    public bool Delete(string path)
    {
        if (!Exists(path)) return false;
        File.Delete(path);
        return true;
    }
}