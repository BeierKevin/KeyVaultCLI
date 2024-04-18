namespace KeyVaultCli.Domain.Services.PasswordHealthCheck;

public class PasswordHealthResult
{
    public bool IsStrong { get; }
    public bool IsUnique { get; }
    public bool IsCompromised { get; }

    public PasswordHealthResult(bool isStrong, bool isUnique, bool isCompromised)
    {
        IsStrong = isStrong;
        IsUnique = isUnique;
        IsCompromised = isCompromised;
    }
}