# =========================
# Build stage
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy everything
COPY . .

# Restore and publish .NET app
RUN dotnet restore
RUN dotnet publish -c Release -o /app

# =========================
# Runtime stage
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy the published app from the build stage
COPY --from=build /app .

# Tell Docker how to start your app
ENTRYPOINT ["dotnet", "ContractMonthlyClaimSystem.dll"]
