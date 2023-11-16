using KeyVaultCli.Application;
using KeyVaultCli.Infrastructure;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Hello, World!");

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure();

using IHost host = builder.Build();

await host.RunAsync();
