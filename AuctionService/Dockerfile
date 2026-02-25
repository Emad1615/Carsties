FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY Carsties.sln ./
COPY AuctionService/AuctionService.csproj ./src/AuctionService/
COPY Contracts/Contracts.csproj ./src/Contracts/

RUN dotnet restore ./src/AuctionService/AuctionService.csproj

COPY . . 
RUN dotnet publish AuctionService/AuctionService.csproj -c Release -o /app/publish

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 80
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "AuctionService.dll"]