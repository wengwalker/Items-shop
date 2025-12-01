## How to run

### Requirements

For local development you can use either pure .NET or Docker. Choose one of the options below.

- **Option A (recommended):** Docker Desktop 4.x+ with Docker Engine and Compose v2.
- **Option B:** Local toolchain:
  - .NET 10 SDK (`10.x`)
  - PostgreSQL 14+ (or a compatible managed instance)

### Configuration

The API host (`ItemsShop.Host`) reads its main database connection string from:

- `ConnectionStrings:Postgres` in `src/Host/ItemsShop.Host/appsettings.json`, or
- the corresponding environment variables when running in Docker.

Default value (used in containerized dev) is:

```json
{
  "ConnectionStrings": {
    "Postgres": "Host=itemsshop-db;Port=5432;Database=itemsshopdb;User Id=postgres;Password=password;Include Error Detail=true;"
  }
}
```

When running locally (without Docker), configure the connection string to point at your PostgreSQL instance, for example via user secrets or environment variables:

- User secrets (from `src/Host/ItemsShop.Host`):

  ```bash
  cd src/Host/ItemsShop.Host
  dotnet user-secrets init
  dotnet user-secrets set "ConnectionStrings:Postgres" "Host=localhost;Port=5432;Database=itemsshopdb;User Id=postgres;Password=postgres;Include Error Detail=true;"
  ```

- Environment variable:
  - Windows PowerShell:

    ```powershell
    $Env:ConnectionStrings__Postgres = "Host=localhost;Port=5432;Database=itemsshopdb;User Id=postgres;Password=postgres;Include Error Detail=true;"
    ```

  - Linux/macOS:

    ```bash
    export ConnectionStrings__Postgres="Host=localhost;Port=5432;Database=itemsshopdb;User Id=postgres;Password=postgres;Include Error Detail=true;"
    ```

#### Environment variables for Docker Compose (dev)

`src/docker-compose.yml` is configured to read environment variables from a `.env` file in `src/` (see `src/example.env`).  
You can copy that file and adjust values as needed:

```bash
cd src
cp example.env .env
```

Key variables:

- **Database**
  - `DATABASE_HOST` – Postgres hostname (default `itemsshop-db`)
  - `DATABASE_INNER_PORT` – Postgres port inside Docker network (default `5432`)
  - `DATABASE_USER`, `DATABASE_PASSWORD`, `DATABASE_DB`
  - `DATABASE_PORTS` – host:container port mapping for Postgres (default `5432:5432`)
- **Host**
  - `HOST_PORTS` – host:container port mapping for the API host (default `8080:8080`)
- **Observability stack**
  - `PROMETHEUS_PORTS`, `GRAFANA_PORTS`, `JAEGER_UI_PORTS`, `SEQ_PORTS`, `SEQ_UI_PORTS`, `OTEL_COLLECTOR_*`

See `src/example.env` for the full list and default values.

### Run — with Docker (recommended)

From the `src/` directory you have two equivalent options.

**Option 1 – via docker compose directly**

```bash
cd src
docker compose up --build
```

**Option 2 – via helper scripts**

In the repository root there are convenience scripts:

- `src/Run.bat` – starts the full Docker stack (`docker compose up --build`).
- `src/Stop.bat` – stops and removes containers and volumes (`docker compose down -v`).

On Windows, you can double‑click these files in Explorer or run from a terminal:

```powershell
cd src
.\Run.bat   # to start
.\Stop.bat  # to stop
```

Then open:

- **API base URL**: `http://localhost:8080`
- **Swagger UI** (Development): `http://localhost:8080/swagger`
- **Prometheus**: `http://localhost:9090`
- **Grafana**: `http://localhost:3000`
- **Jaeger UI**: `http://localhost:16686`
- **Seq UI**: `http://localhost:8081`

To stop:

```bash
cd src
docker compose down -v
```

Notes:

- The API container is named `itemsshop-host` and listens on container port `8080`.
- Migrations for both Catalogs and Orders modules are applied automatically at startup in Development (see `MigrateDatabasesAsync()`).

### Run — locally with .NET 10 (no Docker)

1. Ensure PostgreSQL is running and reachable, and set `ConnectionStrings:Postgres` via user secrets or environment variables (see **Configuration** above).
2. From the `src/` directory, restore the solution:

   ```bash
   cd src
   dotnet restore ItemsShop.sln
   ```

3. Run the host project:

   ```bash
   dotnet run --project Host/ItemsShop.Host/ItemsShop.Host.csproj
   ```

4. Open Swagger UI (Development only) at:

   - `http://localhost:8080/swagger`

If you change models and infrastructure, EF Core migrations for Catalogs and Orders modules should be managed from the respective `Infrastructure` projects. See `docs/development-guide.md` for details.

### API surface (high-level)

The HTTP API is implemented using minimal API endpoints (`IEndpoint` implementations) in:

- `src/Catalogs/ItemsShop.Catalogs.Features/Features/**`
- `src/Orders/ItemsShop.Orders.Features/Features/**`

Main resource groups:

- **Catalogs**
  - `/api/v1/products` – product management (CRUD and partial updates).
  - `/api/v1/categories` – category management.
  - `/api/v1/carts` and `/api/v1/carts/{cartId}/items` – carts and cart items.
- **Orders**
  - `/api/v1/orders` – orders.
  - `/api/v1/orders/{orderId}/items` – order items.

Use Swagger UI to discover detailed schemas and request/response models at runtime. A more complete, static overview is available in `docs/api-reference.md`.
