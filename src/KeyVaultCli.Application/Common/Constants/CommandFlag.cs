namespace KeyVaultCli.Application.Common.Constants;

public enum CommandFlag
{
    // General commands
    Exit = 0,

    // Password related commands
    CreatePassword = 10,
    CreatePasswordGenerated = 11,
    GetPassword = 12,
    GetAllPasswords = 13,
    UpdatePassword = 14,
    UpdatePasswordGenerated = 15,
    DeletePassword = 16,
    DeleteAllPasswords = 17,
    SearchPasswordEntries = 18,

    // Vault related commands
    CreateVault = 20,
    BackupVault = 21,
    RestoreVault = 22,
    DeleteVault = 23,

    // Master password
    UpdateMasterPassword = 30,
}