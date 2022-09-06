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

COPY ["Inventory-Tracker/Inventory-Tracker.csproj", "Inventory-Tracker/"]
# Restore NuGet packages
RUN dotnet restore "Inventory-Tracker/Inventory-Tracker.csproj"
COPY . .
WORKDIR /src/Inventory-Tracker
RUN dotnet sonarscanner begin /k:"IT488" /d:sonar.host.url="https://sonarcube.ultimaengineering.io"  /d:sonar.login="e59632fc6e3537f6ec33ba9910cec4be851372fe"
RUN dotnet build "Inventory-Tracker.csproj" -c Release -o /app/build
RUN dotnet sonarscanner end /d:sonar.login="e59632fc6e3537f6ec33ba9910cec4be851372fe"

FROM build AS publish
RUN dotnet publish "Inventory-Tracker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Inventory-Tracker.dll"]
