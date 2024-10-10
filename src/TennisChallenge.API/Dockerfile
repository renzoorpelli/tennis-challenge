#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TennisChallenge.API/TennisChallenge.API.csproj", "TennisChallenge.API/"]
COPY ["TennisChallenge.Application/TennisChallenge.Application.csproj", "TennisChallenge.Application/"]
COPY ["TennisChallenge.Core/TennisChallenge.Core.csproj", "TennisChallenge.Core/"]
COPY ["TennisChallenge.Infrastructure/TennisChallenge.Infrastructure.csproj", "TennisChallenge.Infrastructure/"]
RUN dotnet restore "./TennisChallenge.API/TennisChallenge.API.csproj"
COPY . .
WORKDIR "/src/TennisChallenge.API"
RUN dotnet build "./TennisChallenge.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TennisChallenge.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TennisChallenge.API.dll"]