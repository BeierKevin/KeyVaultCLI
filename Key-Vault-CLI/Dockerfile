FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Key-Vault-CLI/Key-Vault-CLI.csproj", "Key-Vault-CLI/"]
RUN dotnet restore "Key-Vault-CLI/Key-Vault-CLI.csproj"
COPY . .
WORKDIR "/src/Key-Vault-CLI"
RUN dotnet build "Key-Vault-CLI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Key-Vault-CLI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Key-Vault-CLI.dll"]
