FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore Hotel.API/Hotel.API.csproj
RUN dotnet publish Hotel.API/Hotel.API.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .

EXPOSE 8080

ENTRYPOINT ["dotnet", "Hotel.API.dll"]