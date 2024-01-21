﻿using KeyVaultCli.Application;
using KeyVaultCli.Domain.Entities;

namespace KeyVaultCli.Domain.Commands;

public class GetAllPasswordsCommand : ICommand
{
    private readonly Vault _vault;
    private readonly IConsoleService _consoleService;

    public GetAllPasswordsCommand(Vault vault, IConsoleService consoleService)
    {
        _vault = vault;
        _consoleService = consoleService;
    }

    public void Execute()
    {
        var allPasswordEntries = _vault.LoadPasswordEntries();

        if (allPasswordEntries != null && allPasswordEntries.Any())
        {
            string[] headers = { "GUID", "Service Name", "AccountName", "Password", "Creation Date", "Last Modified Date", };
    
            // Transform every PasswordEntry into a List of objects
            List<List<object>> dataRows = allPasswordEntries
                .Select(entry => new List<object>
                {
                    entry.EntryId,
                    entry.ServiceName, 
                    entry.AccountName, 
                    _vault.GetPassword(entry.ServiceName, entry.AccountName),
                    entry.CreationDate,
                    entry.LastModifiedDate
                }).ToList();

            _consoleService.WriteTable(headers, dataRows);
        }
        else
        {
            _consoleService.WriteError("No password entries found.");
        }
    }
}