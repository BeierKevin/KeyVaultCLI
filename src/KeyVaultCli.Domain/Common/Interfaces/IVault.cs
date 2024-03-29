﻿using KeyVaultCli.Domain.Entities;

namespace KeyVaultCli.Domain.Common.Interfaces;

public interface IVault
{
    public void SaveMasterPassword();

    public string? LoadMasterPassword();

    public bool UpdateMasterPassword(string oldPassword, string newPassword);

    public void AddPasswordEntry(string serviceName, string accountName, string password);

    public void SavePasswordEntries();

    public bool DeletePasswordEntry(string serviceName, string accountName);

    public void DeleteAllPasswordEntries();

    public List<PasswordEntry> LoadPasswordEntries();

    public string GetPassword(string serviceName, string accountName);

    public bool UpdatePasswordEntry(string currentServiceName, string currentAccountName, string newServiceName,
        string newAccountName, int passwordLength, string? newPassword = null);

    public List<PasswordEntry> SearchPasswordEntries(string searchTerm);

    public string GenerateAndAddPasswordEntry(string serviceName, string accountName, int passwordLength);
}