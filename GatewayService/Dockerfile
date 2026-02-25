FROM mcr.microsoft.com/dotnet/sdk:8.0 AS built
WORKDIR /app
# Copy csproj and restore as distinct layers
COPY Carsties.sln ./
COPY GatewayService/GatewayService.csproj ./src/GatewayService/

RUN dotnet restore ./src/GatewayService/GatewayService.csproj

# Copy everything else and build
COPY . .

# Build and publish a release
RUN dotnet publish GatewayService/GatewayService.csproj -c Release -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 80
COPY --from=built /app/publish .
ENTRYPOINT ["dotnet", "GatewayService.dll"]