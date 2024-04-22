# Use the Microsoft .NET SDK image for the build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY src/KeyVaultCli.Presentation/KeyVaultCli.Presentation.csproj src/KeyVaultCli.Presentation/
COPY src/KeyVaultCli.Domain/KeyVaultCli.Domain.csproj src/KeyVaultCli.Domain/
COPY src/KeyVaultCli.Application/KeyVaultCli.Application.csproj src/KeyVaultCli.Application/
COPY src/KeyVaultCli.Infrastructure/KeyVaultCli.Infrastructure.csproj src/KeyVaultCli.Infrastructure/
RUN dotnet restore "src/KeyVaultCli.Presentation/KeyVaultCli.Presentation.csproj"

# Copy the src after restore to use docker build cache and Restore only RUNs when we change the csproj files.
COPY src/ .

# Build the application
WORKDIR "/src/KeyVaultCli.Presentation"
RUN dotnet build "KeyVaultCli.Presentation.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "KeyVaultCli.Presentation.csproj" -c Release -o /app/publish

# Use Microsoft .NET Runtime for final runnable docker image
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set the output assembly name here
# Please replace "THE_NAME_OF_OUTPUT_DLL" with actual output DLL name
ENTRYPOINT ["dotnet", "KeyVaultCli.Presentation.dll"]