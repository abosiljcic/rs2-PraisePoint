FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only the necessary files to the build context
COPY User.GRPC.csproj ./User.GRPC/
COPY ["../User.API/User.API.csproj", "./User.API/"]

# Run dotnet restore
RUN dotnet restore ./User.GRPC/User.GRPC.csproj

# Copy the remaining files
COPY . .

# Build and publish
WORKDIR /src/User.GRPC
RUN dotnet build User.GRPC.csproj -c Release -o /app/build
RUN dotnet publish User.GRPC.csproj -c Release -o /app/publish /p:UseAppHost=false

# Final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "User.GRPC.dll"]
