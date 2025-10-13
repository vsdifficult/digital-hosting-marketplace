#Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy all .csproj files and restore as distinct layers
COPY ["src/HostingMarket.Api/HostingMarket.Api.csproj", "src/HostingMarket.Api/"]
COPY ["src/HostingMarket.Application/HostingMarket.Application.csproj", "src/HostingMarket.Application/"]
COPY ["src/HostingMarket.Domain/HostingMarket.Domain.csproj", "src/HostingMarket.Domain/"]
COPY ["src/HostingMarket.Infrastructure/HostingMarket.Infrastructure.csproj", "src/HostingMarket.Infrastructure/"]

# Copy solution file
COPY ["HostingMarket.sln", "."]

RUN dotnet restore "HostingMarket.sln"

# Copy the rest of the source code
COPY . .
WORKDIR "/src/src/HostingMarket.Api"
RUN dotnet build "HostingMarket.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HostingMarket.Api.csproj" -c Release -o /app/publish

#Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HostingMarket.Api.dll"]