FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /app

# copy csproj and restore as distinct layers
COPY Carsties.sln ./
COPY IdentityService/IdentityService.csproj ./src/IdentityService/

# restore
RUN dotnet restore ./src/IdentityService/IdentityService.csproj
# copy everything else and build app
COPY . .

# publish

RUN dotnet publish IdentityService/IdentityService.csproj -c Release -o /app/publish

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 80
COPY --from=base /app/publish .
ENTRYPOINT ["dotnet", "IdentityService.dll"]