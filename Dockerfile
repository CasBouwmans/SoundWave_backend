# Gebruik de .NET SDK image om de app te bouwen
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Kopieer de projectbestanden en herstel de afhankelijkheden
COPY ["SoundWave APP/SoundWave APP.csproj", "./SoundWave APP/"]
RUN dotnet restore "./SoundWave APP/SoundWave APP.csproj"

# Kopieer de rest van de bestanden en bouw de app
COPY . .
RUN dotnet publish "./SoundWave APP/SoundWave APP.csproj" -c Release -o /app/out

# Gebruik de .NET 8.0 runtime image voor productie
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Exposeer poort 8080
EXPOSE 7283

# Start de applicatie
ENTRYPOINT ["dotnet", "SoundWave APP.dll"]
