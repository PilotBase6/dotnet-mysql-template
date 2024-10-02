FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY ../src/Api/Api.csproj ./src/Api/
COPY ../src/Infrastructure/Infrastructure.csproj ./src/Infrastructure/
RUN dotnet restore "./src/Api/Api.csproj"

COPY ../ .
RUN dotnet publish "./src/Api/Api.csproj" -c Release -o /app/publish
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]