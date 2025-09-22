# How to run

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