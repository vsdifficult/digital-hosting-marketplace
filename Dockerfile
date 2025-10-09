#Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy all .csproj files and restore as distinct layers
COPY ["src/NeuruHub.Api/NeuruHub.Api.csproj", "src/NeuruHub.Api/"]
COPY ["src/NeuruHub.Application/NeuruHub.Application.csproj", "src/NeuruHub.Application/"]
COPY ["src/NeuruHub.Domain/NeuruHub.Domain.csproj", "src/NeuruHub.Domain/"]
COPY ["src/NeuruHub.Infrastructure/NeuruHub.Infrastructure.csproj", "src/NeuruHub.Infrastructure/"]

# Copy solution file
COPY ["NeuruHub.sln", "."]

RUN dotnet restore "NeuruHub.sln"

# Copy the rest of the source code
COPY . .
WORKDIR "/src/src/NeuruHub.Api"
RUN dotnet build "NeuruHub.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NeuruHub.Api.csproj" -c Release -o /app/publish

#Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NeuruHub.Api.dll"]