#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Posts/Posts.API/Posts.API.csproj", "Services/Posts/Posts.API/"]
COPY ["Common/EventBus.Messages/EventBus.Messages.csproj", "Common/EventBus.Messages/"]
COPY ["Services/Posts/Posts.Application/Posts.Application.csproj", "Services/Posts/Posts.Application/"]
COPY ["Services/Posts/Posts.Domain/Posts.Domain.csproj", "Services/Posts/Posts.Domain/"]
COPY ["Services/Posts/Posts.Infrastructure/Posts.Infrastructure.csproj", "Services/Posts/Posts.Infrastructure/"]
RUN dotnet restore "./Services/Posts/Posts.API/Posts.API.csproj"
COPY . .
WORKDIR "/src/Services/Posts/Posts.API"
RUN dotnet build "./Posts.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Posts.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Posts.API.dll"]