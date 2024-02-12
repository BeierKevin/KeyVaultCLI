namespace KeyVaultCli.Domain.Exceptions;

public class PasswordNotFoundException(string serviceName, string accountName)
    : Exception($"Password not found for service '{serviceName}' and account '{accountName}'.");