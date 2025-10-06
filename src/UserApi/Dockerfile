FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Configure HTTPS
ENV ASPNETCORE_URLS=http://+:8080;https://+:8081
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=aspnetapp
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files and restore dependencies
COPY ["src/UserApi/UserApi.csproj", "src/UserApi/"]
COPY ["src/UserCore/UserCore.csproj", "src/UserCore/"]
RUN dotnet restore "src/UserApi/UserApi.csproj"

# Copy source code
COPY . .

# Build the application
WORKDIR "/src/src/UserApi"
RUN dotnet build "UserApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "UserApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserApi.dll"]