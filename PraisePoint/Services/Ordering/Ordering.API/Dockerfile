# Use a basic .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /app

# Copy the csproj files and restore as distinct layers
COPY ["Services/Ordering/Ordering.API/Ordering.API.csproj", "Services/Ordering/Ordering.API/"]
COPY ["Services/Ordering/Ordering.Application/Ordering.Application.csproj", "Services/Ordering/Ordering.Application/"]
COPY ["Services/Ordering/Ordering.Domain/Ordering.Domain.csproj", "Services/Ordering/Ordering.Domain/"]
COPY ["Services/Ordering/Ordering.Infrastructure/Ordering.Infrastructure.csproj", "Services/Ordering/Ordering.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "Services/Ordering/Ordering.API/Ordering.API.csproj"

# Copy the rest of the files
COPY . .

# Build the project
RUN dotnet build "Services/Ordering/Ordering.API/Ordering.API.csproj" -c Release -o /app/build

# Publish the project
RUN dotnet publish "Services/Ordering/Ordering.API/Ordering.API.csproj" -c Release -o /app/publish

# Use a runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory
WORKDIR /app

# Copy the published files from the build stage
COPY --from=build /app/publish .

# Define the entry point
ENTRYPOINT ["dotnet", "Ordering.API.dll"]
