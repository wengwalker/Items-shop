## Dependencies

This document summarizes the main external dependencies used by **Items-shop** and how they are integrated.

> **Note**  
> For the full, authoritative list of package versions, see `Directory.Packages.props` and the individual `.csproj` files.

### Runtime and framework

- **.NET 10 SDK / Runtime**
  - All projects target .NET 10.
  - The host uses ASP.NET Core 10 Web API with minimal API endpoints.

### ASP.NET Core & HTTP stack

- **ASP.NET Core Web API**
  - Used by `ItemsShop.Host` as the primary HTTP stack.
  - Endpoints are defined via minimal API extensions (`MapGet`, `MapPost`, `MapPatch`, `MapDelete`) inside classes implementing `IEndpoint`.

### Data access

- **Entity Framework Core**
  - Used in module infrastructure projects (`ItemsShop.Catalogs.Infrastructure`, `ItemsShop.Orders.Infrastructure`).
  - Provides:
    - DbContext and entity mapping for Catalogs and Orders.
    - Migrations for evolving the PostgreSQL schema.
  - Migrations are automatically applied at startup in Development via `MigrateDatabasesAsync()`.

- **Npgsql (PostgreSQL provider)**
  - EF Core provider for PostgreSQL.
  - Connection string is provided via `ConnectionStrings:Postgres` in `ItemsShop.Host` configuration.

### Validation

- **FluentValidation**
  - Used to validate request models at the feature level.
  - Each endpoint’s handler typically validates a `Request` record before executing business logic.
  - Validation failures are mapped to standardized ProblemDetails responses.

### API documentation

- **Swashbuckle / Swagger**
  - Configured in `ItemsShop.Common.Api` via `AddSwaggerGen`.
  - Generates OpenAPI specification and exposes:
    - Swagger JSON.
    - Swagger UI at `/swagger` (Development environment only).
  - Endpoint metadata (`WithSummary`, `WithDescription`, `Produces*`) is used to enrich the OpenAPI schema.

### Observability and logging

- **Microsoft.Extensions.Logging**
  - Base logging abstraction used by the host and modules.

- **OpenTelemetry**
  - Integrated in `ItemsShop.Common.Api` and `ItemsShop.Common.Infrastructure`.
  - Used for:
    - Structured logs enriched with trace/span context.
    - Distributed tracing.
    - Metrics emission.
  - Logs, traces and metrics are exported via OTLP to the OpenTelemetry Collector running in Docker.

- **OpenTelemetry Collector**
  - Configured in `Configs/otel-collector/otel-collector-config.yaml`.
  - Receives logs/traces/metrics from the API host.
  - Exposes Prometheus scrape endpoint for metrics.

- **Prometheus**
  - Configured in `Configs/prometheus/prometheus.yml`.
  - Scrapes metrics from the OpenTelemetry Collector.

- **Grafana**
  - Uses provisioned dashboards (under `Configs/grafana/dashboards`) to visualize metrics.
  - Anonymous access can be enabled for local development.

- **Jaeger**
  - Receives and visualizes distributed traces.

- **Seq**
  - Collects and displays structured logs emitted by the host.

All of the above observability services are wired via `docker-compose.yml`.

### Health checks

- **ASP.NET Core HealthChecks**
  - Registered and mapped via `ItemsShop.Common.Api` extensions.
  - A health endpoint is exposed (used by Docker and Prometheus).
  - `HealthChecksUI` configuration in `ItemsShop.Host/appsettings.json` is used to monitor health from a UI.

### Testing

Testing-specific dependencies (for example, xUnit, FluentAssertions) may be present if test projects are added. Refer to test `.csproj` files (if any) for details.

### Tooling

- **Docker & Docker Compose**
  - `docker-compose.yml` orchestrates:
    - PostgreSQL.
    - OpenTelemetry Collector.
    - Prometheus.
    - Grafana.
    - Jaeger.
    - Seq.
    - `itemsshop-host` API container.
  - `example.env` provides default environment variables for local development.

- **EF Core Tools**
  - `dotnet-ef` is used to add and apply migrations from the Infrastructure projects.
  - Not required at runtime but recommended for development workflows.

If you introduce new dependencies, update this document and ensure they are consistent with the existing architecture (for example, by keeping cross-cutting concerns in `Common` projects and module-specific concerns in their respective projects).
