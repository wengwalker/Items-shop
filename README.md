# Items-shop

A simple web marketplace implemented in ASP.NET Core 8 (Web API) with Entity Framework Core (PostgreSQL), Serilog, FluentValidation, and Swagger/OpenAPI. The service exposes product, category, cart, and cart item operations and ships with Docker support for easy local development.

## Features

- Products and Categories CRUD
- Carts and Cart Items management
- PostgreSQL with EF Core migrations applied automatically at startup
- Request logging with Serilog
- Validation with FluentValidation
- Swagger UI for API exploration in Development
- Dockerfile and docker-compose for local development

## Tech stack

- **Runtime**: .NET 8
- **Web**: ASP.NET Core Web API
- **Database**: PostgreSQL (Npgsql provider)
- **ORM**: Entity Framework Core 8
- **Migrations**: EF Core Code-First (auto-applied on startup)
- **Validation**: FluentValidation
- **Mediation**: Mediator.Lite
- **Logging**: Serilog (Console sink)
- **API Docs**: Swashbuckle (Swagger/OpenAPI)

## Requirements

For local development you can use either pure .NET or Docker. Choose one of the options below.

- **Option A (recommended):** Docker Desktop 4.x+
  - Docker Engine + Compose v2
- **Option B:** Local toolchain
  - .NET 8 SDK (`8.x`)
  - PostgreSQL 14+ (or a compatible managed instance)

## Configuration

The API reads its connection string under `ConnectionStrings:CatalogDbContext`.

Defaults (used in containerized dev):

```json
// src/Catalog/Catalog.Api/appsettings.json
{
  "ConnectionStrings": {
    "CatalogDbContext": "Host=catalog-db;Port=5432;Database=catalogDb;User Id=postgres1;Password=password1;Include Error Detail=true;"
  }
}
```

When running locally (without Docker), set the connection string to point at your PostgreSQL instance, for example via user secrets or environment variables:

- User secrets (from `src/Catalog/Catalog.Api`):

  ```bash
  dotnet user-secrets init
  dotnet user-secrets set "ConnectionStrings:CatalogDbContext" "Host=localhost;Port=5432;Database=catalogDb;User Id=postgres;Password=postgres;Include Error Detail=true;"
  ```

- Environment variable:
  - Windows PowerShell:

    ```powershell
    $Env:ConnectionStrings__CatalogDbContext = "Host=localhost;Port=5432;Database=catalogDb;User Id=postgres;Password=postgres;Include Error Detail=true;"
    ```

  - Linux/macOS:

    ```bash
    export ConnectionStrings__CatalogDbContext="Host=localhost;Port=5432;Database=catalogDb;User Id=postgres;Password=postgres;Include Error Detail=true;"
    ```

### Environment variables for docker-compose (dev)

`src/docker-compose.development.yml` expects the following variables (you can place them in a `.env` file at `src/.env`):

```env
# PostgreSQL
DATABASE_CATALOG_HOST=catalog-db
DATABASE_CATALOG_INNER_PORT=5432
DATABASE_CATALOG_USER=postgres1
DATABASE_CATALOG_PASSWORD=password1
DATABASE_CATALOG_DB=catalogDb

# Host port mappings (host:container)
DATABASE_CATALOG_PORTS=5432:5432
CATALOG_PORTS=8080:8080
```

Notes:

- The API container listens on port `8080` (see `src/Catalog/Dockerfile`).
- Swagger UI is available in Development at `/swagger`.
- Migrations run automatically on startup (see `MigrateDatabase<T>()`). Ensure the database container is healthy before the API starts (compose `depends_on` already enforces this).

## Run — with Docker (recommended)

From the `src/` directory:

```bash
cd src
docker compose -f docker-compose.development.yml up --build
```

Then open:

- API base URL: `http://localhost:8080`
- Swagger UI: `http://localhost:8080/swagger`

To stop:

```bash
docker compose -f docker-compose.development.yml down -v
```

## Run — locally with .NET (no Docker)

1) Ensure PostgreSQL is running and reachable, and set `ConnectionStrings:CatalogDbContext` via user-secrets or environment variables (see Configuration above).

2) Restore and run the API:

```bash
dotnet restore items-shop.sln
dotnet run --project src/Catalog/Catalog.Api/Catalog.Api.csproj
```

Open Swagger UI at `http://localhost:{specified-port}/swagger` (or the HTTPS equivalent).

## API surface (high-level)

Controllers available under `src/Catalog/Catalog.Api/Controllers`:

- `ProductsController` — CRUD for products and category association
- `CategoriesController` — CRUD for categories
- `CartsController` — CRUD for carts
- `CartItemsController` — CRUD for items in a cart

Use Swagger to discover schemas and request/response models at runtime.

## Development notes

- Migrations are applied automatically at startup. If you change the model and want to create a new migration locally:

  ```bash
  cd src/Catalog/Catalog.Infrastructure
  dotnet-ef migrations add <Name> -p .\Catalog.Infrastructure.csproj -s ..\Catalog.Api\Catalog.Api.csproj -o .\Migrations\
  ```

  Ensure you have the EF Core tools installed: `dotnet tool install --global dotnet-ef`.

- Logging via Serilog is configured in `appsettings.json` → `Serilog`. Adjust sinks/levels as needed.

## Educational purpose

This repository is a learning project aimed at exploring technologies (ASP.NET Core, EF Core, PostgreSQL, Serilog, validation/mediator patterns, containerization, etc.). It does not strive for perfect architecture or production-grade code quality. Decisions are intentionally optimized for clarity and experimentation rather than completeness or robustness.

## License

This project is licensed under the MIT License. See `LICENSE` for details.
