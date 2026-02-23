FROM mcr/microsoft/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project file and restore dependencies

COPY Carsties.sln ./
COPY AuctionService/AuctionService.csproj ./src/AuctionService/
COPY Contracts/Contracts.csproj ./src/Contracts/

RUN dotnet restore ./src/AuctionService/AuctionService.csproj

# Copy all resources
COPY ..	
WORKDIR /app/src/AuctionService
RUN dotnet publish -c Release -o /app/publish

