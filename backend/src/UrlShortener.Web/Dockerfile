# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5000
EXPOSE 5001


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["src/UrlShortener.Web/UrlShortener.Web.csproj", "src/UrlShortener.Web/"]
COPY ["src/UrlShortener.Application/UrlShortener.Application.csproj", "src/UrlShortener.Application/"]
COPY ["src/UrlShortener.Domain/UrlShortener.Domain.csproj", "src/UrlShortener.Domain/"]
COPY ["src/UrlShortener.Infrastructure/UrlShortener.Infrastructure.csproj", "src/UrlShortener.Infrastructure/"]
COPY ["src/UrlShortener.Persistence/UrlShortener.Persistence.csproj", "src/UrlShortener.Persistence/"]
COPY ["src/UrlShortener.Presentation/UrlShortener.Presentation.csproj", "src/UrlShortener.Presentation/"]
RUN dotnet restore "./src/UrlShortener.Web/UrlShortener.Web.csproj"
COPY . .
WORKDIR "/src/src/UrlShortener.Web"
RUN dotnet build "./UrlShortener.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UrlShortener.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_HTTP_PORTS=5000
ENTRYPOINT ["dotnet", "UrlShortener.Web.dll"]