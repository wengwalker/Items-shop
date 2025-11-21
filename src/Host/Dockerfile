FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine3.21 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine3.21 AS build
WORKDIR /src
COPY ["Directory.Packages.props", "Directory.Build.props", "./"]
COPY ["Host/ItemsShop.Host/ItemsShop.Host.csproj", "Host/ItemsShop.Host/"]
COPY ["Common/ItemsShop.Common.Api/ItemsShop.Common.Api.csproj", "Common/ItemsShop.Common.Api/"]
COPY ["Common/ItemsShop.Common.Application/ItemsShop.Common.Application.csproj", "Common/ItemsShop.Common.Application/"]
COPY ["Common/ItemsShop.Common.Domain/ItemsShop.Common.Domain.csproj", "Common/ItemsShop.Common.Domain/"]
COPY ["Common/ItemsShop.Common.Infrastructure/ItemsShop.Common.Infrastructure.csproj", "Common/ItemsShop.Common.Infrastructure/"]
COPY ["Catalogs/ItemsShop.Catalogs.Domain/ItemsShop.Catalogs.Domain.csproj", "Catalogs/ItemsShop.Catalogs.Domain/"]
COPY ["Catalogs/ItemsShop.Catalogs.Features/ItemsShop.Catalogs.Features.csproj", "Catalogs/ItemsShop.Catalogs.Features/"]
COPY ["Catalogs/ItemsShop.Catalogs.Infrastructure/ItemsShop.Catalogs.Infrastructure.csproj", "Catalogs/ItemsShop.Catalogs.Infrastructure/"]
COPY ["Catalogs/ItemsShop.Catalogs.PublicApi/ItemsShop.Catalogs.PublicApi.csproj", "Catalogs/ItemsShop.Catalogs.PublicApi/"]
COPY ["Orders/ItemsShop.Orders.Domain/ItemsShop.Orders.Domain.csproj", "Orders/ItemsShop.Orders.Domain/"]
COPY ["Orders/ItemsShop.Orders.Features/ItemsShop.Orders.Features.csproj", "Orders/ItemsShop.Orders.Features/"]
COPY ["Orders/ItemsShop.Orders.Infrastructure/ItemsShop.Orders.Infrastructure.csproj", "Orders/ItemsShop.Orders.Infrastructure/"]
RUN dotnet restore "Host/ItemsShop.Host/ItemsShop.Host.csproj"

COPY ["Host/ItemsShop.Host", "Host/ItemsShop.Host"]
COPY ["Common/ItemsShop.Common.Api", "Common/ItemsShop.Common.Api"]
COPY ["Common/ItemsShop.Common.Application", "Common/ItemsShop.Common.Application"]
COPY ["Common/ItemsShop.Common.Domain", "Common/ItemsShop.Common.Domain"]
COPY ["Common/ItemsShop.Common.Infrastructure", "Common/ItemsShop.Common.Infrastructure"]
COPY ["Catalogs/ItemsShop.Catalogs.Domain", "Catalogs/ItemsShop.Catalogs.Domain"]
COPY ["Catalogs/ItemsShop.Catalogs.Features", "Catalogs/ItemsShop.Catalogs.Features"]
COPY ["Catalogs/ItemsShop.Catalogs.Infrastructure", "Catalogs/ItemsShop.Catalogs.Infrastructure"]
COPY ["Catalogs/ItemsShop.Catalogs.PublicApi", "Catalogs/ItemsShop.Catalogs.PublicApi"]
COPY ["Orders/ItemsShop.Orders.Domain", "Orders/ItemsShop.Orders.Domain"]
COPY ["Orders/ItemsShop.Orders.Features", "Orders/ItemsShop.Orders.Features"]
COPY ["Orders/ItemsShop.Orders.Infrastructure", "Orders/ItemsShop.Orders.Infrastructure"]
WORKDIR /src/Host/ItemsShop.Host
ARG BUILD_CONFIGURATION=Development
RUN dotnet build "./ItemsShop.Host.csproj" -c ${BUILD_CONFIGURATION} -o /app/build --no-restore

FROM build AS publish
WORKDIR /src/Host/ItemsShop.Host
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "./ItemsShop.Host.csproj" -c ${BUILD_CONFIGURATION} -o "/app/publish" /p:UseAppHost=false --no-restore

FROM base AS runtime
WORKDIR /app
COPY --from=publish "/app/publish" .
ENTRYPOINT [ "dotnet", "ItemsShop.Host.dll" ]
