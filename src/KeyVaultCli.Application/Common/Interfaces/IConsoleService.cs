﻿using KeyVaultCli.Domain;

namespace KeyVaultCli.Application.Common.Interfaces;

public interface IConsoleService : IConsole
{
    new string? GetInput(string prompt);

    new void WriteText(string message);

    new void WriteInfo(string message);

    new void WriteSuccess(string message);

    new void WriteError(string message);

    new void WriteTable(string[] headers, List<List<object>> data);

    new void Clear();
}