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
COPY ["src/Payment.Api/Payment.Api.csproj", "src/Payment.Api/"]
COPY ["src/Payment.Business/Payment.Business.csproj", "src/Payment.Business/"]
COPY ["src/Payment.Repository/Payment.Repository.csproj", "src/Payment.Repository/"]
COPY ["src/Payment.Shared/Payment.Shared.csproj", "src/Payment.Shared/"]
RUN dotnet restore "./src/Payment.Api/./Payment.Api.csproj"
COPY . .
WORKDIR "/src/src/Payment.Api"
RUN dotnet build "./Payment.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Payment.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Payment.Api.dll"]