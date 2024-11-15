#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NuGet.Config", "."]
COPY ["src/Warehouse.Api/Warehouse.Api.csproj", "src/Warehouse.Api/"]
COPY ["src/Warehouse.Shared/Warehouse.Shared.csproj", "src/Warehouse.Shared/"]
COPY ["src/Warehouse.Repository/Warehouse.Repository.csproj", "src/Warehouse.Repository/"]
COPY ["src/Warehouse.Business/Warehouse.Business.csproj", "src/Warehouse.Business/"]
RUN dotnet restore "./src/Warehouse.Api/./Warehouse.Api.csproj"
COPY . .
WORKDIR "/src/src/Warehouse.Api"
RUN dotnet build "./Warehouse.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Warehouse.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Warehouse.Api.dll"]