using KeyVaultCli.Application.Common.Interfaces;

namespace KeyVaultCli.Domain.UnitTests.Fakes;

public class FakeFileService : IFileService
{
    private Dictionary<string, string> _files = new();

    public bool Exists(string path)
    {
        return _files.ContainsKey(path);
    }

    public void WriteAllText(string path, string content)
    {
        if (_files.ContainsKey(path))
        {
            _files[path] = content;
        }
        else 
        {
            _files.Add(path, content);
        }
    }

    public string ReadAllText(string path)
    {
        return _files.TryGetValue(path, out var content) ? content : string.Empty;
    }

    public bool Delete(string path)
    {
        return _files.Remove(path);
    }
}