#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8083

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/services/Fiap.TechChallenge.Listagem.API/Fiap.TechChallenge.Listagem.API.csproj", "src/services/Fiap.TechChallenge.Listagem.API/"]
COPY ["src/blocks/Fiap.TechChallenge.Api/Fiap.TechChallenge.Api.csproj", "src/blocks/Fiap.TechChallenge.Api/"]
COPY ["src/blocks/Fiap.TechChallenge.Infrastructure/Fiap.TechChallenge.Infrastructure.csproj", "src/blocks/Fiap.TechChallenge.Infrastructure/"]
COPY ["src/blocks/Fiap.TechChallenge.Application/Fiap.TechChallenge.Application.csproj", "src/blocks/Fiap.TechChallenge.Application/"]
COPY ["src/blocks/Fiap.TechChallenge.Kernel/Fiap.TechChallenge.Kernel.csproj", "src/blocks/Fiap.TechChallenge.Kernel/"]
RUN dotnet restore "./src/services/Fiap.TechChallenge.Listagem.API/Fiap.TechChallenge.Listagem.API.csproj"
COPY . .
WORKDIR "/src/src/services/Fiap.TechChallenge.Listagem.API"
RUN dotnet build "./Fiap.TechChallenge.Listagem.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Fiap.TechChallenge.Listagem.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fiap.TechChallenge.Listagem.API.dll"]