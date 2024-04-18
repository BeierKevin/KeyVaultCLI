using KeyVaultCli.Domain.Common.Interfaces;

namespace KeyVaultCli.Domain.Services.PasswordHealthCheck;

public class PasswordHealthService
{
    private readonly IPasswordStrengthService _passwordStrengthService;
    private readonly IPasswordUniquenessService _passwordUniquenessService;
    private readonly ICompromisedPasswordService _compromisedPasswordService;

    public PasswordHealthService(IPasswordStrengthService passwordStrengthService,
        IPasswordUniquenessService passwordUniquenessService,
        ICompromisedPasswordService compromisedPasswordService)
    {
        _passwordStrengthService = passwordStrengthService;
        _passwordUniquenessService = passwordUniquenessService;
        _compromisedPasswordService = compromisedPasswordService;
    }

    public async Task<PasswordHealthResult> CheckPasswordHealthAsync(string password)
    {
        var isStrong = _passwordStrengthService.IsStrongPassword(password);
        var isUnique = await _passwordUniquenessService.IsUniquePasswordAsync(password);
        var isCompromised = await _compromisedPasswordService.HasBeenCompromisedAsync(password);

        return new PasswordHealthResult(isStrong, isUnique, isCompromised);
    }
}