#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Start Sonar Scanner
RUN apt-get update && apt-get install -y openjdk-11-jdk
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet tool install --global dotnet-sonarscanner
RUN dotnet tool install --global coverlet.console
RUN dotnet sonarscanner begin /k:"IT488" /d:sonar.host.url="https://sonarcube.ultimaengineering.io"  /d:sonar.login="9a8fd0cb67f74e916fc90cff70f53a4e145e5529"

COPY ["Inventory-Tracker/Inventory-Tracker.csproj", "Inventory-Tracker/"]
# Restore NuGet packages
RUN dotnet restore "Inventory-Tracker/Inventory-Tracker.csproj"
COPY . .
WORKDIR "/src/Inventory-Tracker"
RUN dotnet build "Inventory-Tracker.csproj" -c Release -o /app/build


FROM build AS publish
WORKDIR /src
RUN dotnet publish "Inventory-Tracker.csproj" -c Release -o /app/publish
RUN dotnet sonarscanner end /d:sonar.login="9a8fd0cb67f74e916fc90cff70f53a4e145e5529"

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Inventory-Tracker.dll"]
