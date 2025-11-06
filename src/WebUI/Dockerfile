FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine3.21 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine3.21 AS build
WORKDIR /src
COPY ["Directory.Packages.props", "Directory.Build.props", "./"]
COPY ["./WebUI/ItemsShop.WebUI/ItemsShop.WebUI.csproj", "./WebUI/ItemsShop.WebUI/"]
RUN dotnet restore "./WebUI/ItemsShop.WebUI/ItemsShop.WebUI.csproj"

COPY ["./WebUI/ItemsShop.WebUI", "./WebUI/ItemsShop.WebUI"]
WORKDIR /src/WebUI/ItemsShop.WebUI
ARG BUILD_CONFIGURATION=Development
RUN dotnet build "./ItemsShop.WebUI.csproj" -c ${BUILD_CONFIGURATION} -o /app/build --no-restore

FROM build AS publish
WORKDIR /src/WebUI/ItemsShop.WebUI
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "./ItemsShop.WebUI.csproj" -c ${BUILD_CONFIGURATION} -o "/app/publish" /p:UseAppHost=false --no-restore

FROM base AS runtime
WORKDIR /app
COPY --from=publish "/app/publish" .
ENTRYPOINT [ "dotnet", "ItemsShop.WebUI.dll" ]


