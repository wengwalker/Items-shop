FROM mcr.microsoft.com/dotnet/aspnet:8.0 as base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /src
COPY ["./*.props", "./"]
COPY ["./Catalog/Catalog.Api/Catalog.Api.csproj", "./Catalog/Catalog.Api/"]
COPY ["./Catalog/Catalog.Application/Catalog.Application.csproj", "./Catalog/Catalog.Application/"]
COPY ["./Catalog/Catalog.Domain/Catalog.Domain.csproj", "./Catalog/Catalog.Domain/"]
COPY ["./Catalog/Catalog.Infrastructure/Catalog.Infrastructure.csproj", "./Catalog/Catalog.Infrastructure/"]
COPY ["./Shared/Api.Common/Api.Common.csproj", "./Shared/Api.Common/"]
COPY ["./Shared/Domain.Common/Domain.Common.csproj", "./Shared/Domain.Common/"]
COPY ["./Shared/Infrastructure.Common/Infrastructure.Common.csproj", "./Shared/Infrastructure.Common/"]
RUN dotnet restore "./Catalog/Catalog.Api/Catalog.Api.csproj"

COPY ["./Catalog/Catalog.Api", "./Catalog/Catalog.Api"]
COPY ["./Catalog/Catalog.Application", "./Catalog/Catalog.Application"]
COPY ["./Catalog/Catalog.Domain", "./Catalog/Catalog.Domain"]
COPY ["./Catalog/Catalog.Infrastructure", "./Catalog/Catalog.Infrastructure"]
COPY ["./Shared/Api.Common", "./Shared/Api.Common"]
COPY ["./Shared/Domain.Common", "./Shared/Domain.Common"]
COPY ["./Shared/Infrastructure.Common", "./Shared/Infrastructure.Common"]
WORKDIR /src/Catalog/Catalog.Api
ARG BUILD_CONFIGURATION=Development
RUN dotnet build "./Catalog.Api.csproj" -c ${BUILD_CONFIGURATION} -o /app/build --no-restore

FROM build AS publish
WORKDIR /src/Catalog/Catalog.Api
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "./Catalog.Api.csproj" -c ${BUILD_CONFIGURATION} -o "/app/publish" /p:UseAppHost=false --no-restore

FROM base AS runtime
WORKDIR /app
COPY --from=publish "/app/publish" .
ENTRYPOINT [ "dotnet", "Catalog.Api.dll" ]
