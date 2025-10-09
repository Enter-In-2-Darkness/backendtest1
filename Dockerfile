FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project file
COPY backendtest1/backendtest1.csproj backendtest1/
RUN dotnet restore backendtest1/backendtest1.csproj

# Copy everything
COPY . .

# Build
WORKDIR /src/backendtest1
RUN dotnet publish backendtest1.csproj -c Release -o /app/publish

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Port configuration
ENV ASPNETCORE_URLS=http://+:$PORT
EXPOSE $PORT

# Run
CMD ASPNETCORE_URLS=http://+:$PORT dotnet backendtest1.dll