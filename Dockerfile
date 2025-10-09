# ใช้ PowerShell
@"
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project file
COPY ["backendtest1/backendtest1/backendtest1.csproj", "backendtest1/backendtest1/"]
RUN dotnet restore "backendtest1/backendtest1/backendtest1.csproj"

# Copy All files
COPY . .

# Build
WORKDIR "/src/backendtest1/backendtest1"
RUN dotnet publish "backendtest1.csproj" -c Release -o /app/publish

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:`$PORT
EXPOSE `$PORT

CMD ASPNETCORE_URLS=http://+:`$PORT dotnet backendtest1.dll
"@ | Out-File -FilePath Dockerfile -Encoding ASCII