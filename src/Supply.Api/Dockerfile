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
COPY ["src/Supply.Api/Supply.Api.csproj", "src/Supply.Api/"]
COPY ["src/Supply.Shared/Supply.Shared.csproj", "src/Supply.Shared/"]
COPY ["src/Supply.Business/Supply.Business.csproj", "src/Supply.Business/"]
COPY ["src/Supply.Repository/Supply.Repository.csproj", "src/Supply.Repository/"]
RUN dotnet restore "./src/Supply.Api/./Supply.Api.csproj"
COPY . .
WORKDIR "/src/src/Supply.Api"
RUN dotnet build "./Supply.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Supply.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Supply.Api.dll"]