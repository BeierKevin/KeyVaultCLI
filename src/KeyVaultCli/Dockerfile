FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/KeyVaultCli/KeyVaultCli.csproj", "src/KeyVaultCli/"]
RUN dotnet restore "src/KeyVaultCli/KeyVaultCli.csproj"
COPY . .
WORKDIR "/src/src/KeyVaultCli"
RUN dotnet build "KeyVaultCli.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KeyVaultCli.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KeyVaultCli.dll"]