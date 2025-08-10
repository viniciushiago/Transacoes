# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o csproj e restaura dependências para acelerar o build
COPY ["Transacoes/Transacoes.csproj", "Transacoes/"]
RUN dotnet restore "Transacoes/Transacoes.csproj"

# Copia todo o código e publica
COPY . .
WORKDIR /src/Transacoes
RUN dotnet publish "Transacoes.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Transacoes.dll"]
