FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /source

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore "./BuberBreakfast/BuberBreakfast.csproj"
# Build and publish a release
RUN dotnet publish "./BuberBreakfast/BuberBreakfast.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /source/out .

EXPOSE 5000

ENTRYPOINT ["dotnet", "BuberBreakfast.dll"]