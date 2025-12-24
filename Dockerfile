#Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy all .csproj files and restore as distinct layers
COPY ["src/HostMarket.Api/HostMarket.Api.csproj", "src/HostMarket.Api/"]
COPY ["src/HostMarket.Core/HostMarket.Core.csproj", "src/HostMarket.Core/"]
COPY ["src/HostMarket.Infrastructure/HostMarket.Infrastructure.csproj", "src/HostMarket.Infrastructure/"]
COPY ["src/HostMarket.Shared/HostMarket.Shared.csproj", "src/HostMarket.Shared/"]

# Copy solution file
COPY ["digital-hosting-marketplace.sln", "."]

RUN dotnet restore "digital-hosting-marketplace.sln"

# Copy the rest of the source code
COPY . .
WORKDIR "/src/src/HostMarket.Api"
RUN dotnet build "HostMarket.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HostMarket.Api.csproj" -c Release -o /app/publish

#Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HostMarket.Api.dll"]
