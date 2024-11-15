#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 443

RUN mkdir -p /app/.data && chmod -R 0777 /app/.data

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NuGet.Config", "."]
COPY ["src/MicroShop.Webapp/MicroShop.Webapp.csproj", "src/MicroShop.Webapp/"]
COPY ["src/MicroShop.Business/MicroShop.Business.csproj", "src/MicroShop.Business/"]
COPY ["src/MicroShop.Repository/MicroShop.Repository.csproj", "src/MicroShop.Repository/"]
COPY ["src/MicroShop.Shared/MicroShop.Shared.csproj", "src/MicroShop.Shared/"]
RUN dotnet restore "./src/MicroShop.Webapp/./MicroShop.Webapp.csproj"
COPY . .
WORKDIR "/src/src/MicroShop.Webapp"
RUN dotnet build "./MicroShop.Webapp.csproj" -c $BUILD_CONFIGURATION -o /app/build
RUN dotnet dev-certs https

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MicroShop.Webapp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS="https://+:8080;https://+:8081;"
ENTRYPOINT ["dotnet", "MicroShop.Webapp.dll"]