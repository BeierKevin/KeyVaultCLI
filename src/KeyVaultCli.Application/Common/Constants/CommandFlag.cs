namespace KeyVaultCli.Application.PasswordEntry.Common.Constants;

public enum CommandFlag
{
    CreatePassword = 1,
    CreatePasswordGenerated = 11,
    GetPassword = 2,
    GetAllPasswords = 3,
    UpdatePassword = 4,
    UpdatePasswordGenerated = 44,
    DeletePassword = 5,
    SearchPasswordEntries = 6,
    UpdateMasterPassword = 7,
    Exit = 0,
    DeleteAllPasswords = -1
}