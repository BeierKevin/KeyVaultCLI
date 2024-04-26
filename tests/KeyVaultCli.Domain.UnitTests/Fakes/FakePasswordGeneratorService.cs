using KeyVaultCli.Application.Common.Interfaces;

namespace KeyVaultCli.Domain.UnitTests.Fakes;

public class FakePasswordGeneratorService : IPasswordGeneratorService
{
    public string GeneratePassword(int length)
    {
        return new String('a', length); // return a constant string of 'a's of length 
    }
}