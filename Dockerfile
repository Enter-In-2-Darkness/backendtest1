FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY backendtest1/*.csproj backendtest1/
RUN dotnet restore backendtest1/backendtest1.csproj
COPY . .
WORKDIR /src/backendtest1
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "backendtest1.dll"]
