# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj files and restore
COPY src/Web/Web.csproj ./src/Web/
COPY src/Web/Web.Client/Web.Client.csproj ./src/Web/Web.Client/
RUN dotnet restore ./src/Web/Web.csproj

# Copy everything and publish
COPY . .
RUN dotnet publish ./src/Web/Web.csproj -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "Web.dll"]