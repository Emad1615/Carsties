FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
# Copy the project file and restore dependencies
COPY Carsties.sln ./
COPY SearchService/SearchService.csproj ./src/SearchService/
COPY Contracts/Contracts.csproj ./src/Contracts/

RUN dotnet restore ./src/SearchService/SearchService.csproj

# copy the rest of the source code
COPY . .

# publish the application
RUN dotnet publish SearchService/SearchService.csproj -c Release -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 80
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SearchService.dll"]