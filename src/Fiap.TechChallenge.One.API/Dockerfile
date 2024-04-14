#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Fiap.TechChallenge.One.API/Fiap.TechChallenge.One.API.csproj", "src/Fiap.TechChallenge.One.API/"]
COPY ["src/Fiap.TechChallenge.One.Infrastructure/Fiap.TechChallenge.One.Infrastructure.csproj", "src/Fiap.TechChallenge.One.Infrastructure/"]
COPY ["src/Fiap.TechChallenge.One.Application/Fiap.TechChallenge.One.Application.csproj", "src/Fiap.TechChallenge.One.Application/"]
COPY ["src/Fiap.TechChallenge.One.Domain/Fiap.TechChallenge.One.Domain.csproj", "src/Fiap.TechChallenge.One.Domain/"]
RUN dotnet restore "./src/Fiap.TechChallenge.One.API/Fiap.TechChallenge.One.API.csproj"
COPY . .
WORKDIR "/src/src/Fiap.TechChallenge.One.API"
RUN dotnet build "./Fiap.TechChallenge.One.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Fiap.TechChallenge.One.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fiap.TechChallenge.One.API.dll"]