FROM mcr.microsoft.com/dotnet/aspnet:8.0 as base
WORKDIR /app
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /src
COPY ["./*.props", "./"]
COPY ["./Order/Order.Api/Order.Api.csproj", "./Order/Order.Api/"]
COPY ["./Order/Order.Application/Order.Application.csproj", "./Order/Order.Application/"]
COPY ["./Order/Order.Domain/Order.Domain.csproj", "./Order/Order.Domain/"]
COPY ["./Order/Order.Infrastructure/Order.Infrastructure.csproj", "./Order/Order.Infrastructure/"]
COPY ["./Shared/Api.Common/Api.Common.csproj", "./Shared/Api.Common/"]
COPY ["./Shared/Domain.Common/Domain.Common.csproj", "./Shared/Domain.Common/"]
COPY ["./Shared/Infrastructure.Common/Infrastructure.Common.csproj", "./Shared/Infrastructure.Common/"]
RUN dotnet restore "./Order/Order.Api/Order.Api.csproj"

COPY ["./Order/Order.Api", "./Order/Order.Api"]
COPY ["./Order/Order.Application", "./Order/Order.Application"]
COPY ["./Order/Order.Domain", "./Order/Order.Domain"]
COPY ["./Order/Order.Infrastructure", "./Order/Order.Infrastructure"]
COPY ["./Shared/Api.Common", "./Shared/Api.Common"]
COPY ["./Shared/Domain.Common", "./Shared/Domain.Common"]
COPY ["./Shared/Infrastructure.Common", "./Shared/Infrastructure.Common"]
WORKDIR /src/Order/Order.Api
ARG BUILD_CONFIGURATION=Development
RUN dotnet build "./Order.Api.csproj" -c ${BUILD_CONFIGURATION} -o /app/build --no-restore

FROM build AS publish
WORKDIR /src/Order/Order.Api
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "./Order.Api.csproj" -c ${BUILD_CONFIGURATION} -o "/app/publish" /p:UseAppHost=false --no-restore

FROM base AS runtime
WORKDIR /app
COPY --from=publish "/app/publish" .
ENTRYPOINT [ "dotnet", "Order.Api.dll" ]
