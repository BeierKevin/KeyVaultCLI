using KeyVaultCli.Application.Common.Interfaces;

namespace KeyVaultCli.Domain.UnitTests.Fakes;

public class SpyFileService : IFileService
{
    public int WriteCallCount { get; private set; }

    public bool Exists(string path) { return true; }
    public void WriteAllText(string path, string content) { WriteCallCount++; }
    public string ReadAllText(string path) { return string.Empty; }
    public bool Delete(string path) { return true; }
}