## Items-shop

Items-shop is a modular ASP.NET Core Web API that implements a simple marketplace domain.  
The solution is organized into reusable modules (Catalogs and Orders) and a single host that wires them together, with observability (logs, metrics, traces) and Docker-based infrastructure for local development.

### Features

- **Catalogs module**: Products and Categories CRUD, Carts and Cart Items management
- **Orders module**: Orders and Order Items management
- **Health & diagnostics**: Health checks, structured logging, distributed tracing, metrics dashboards
- **Database & ORM**: PostgreSQL with Entity Framework Core migrations applied automatically in Development
- **Validation & error handling**: FluentValidation + ProblemDetails-based error responses
- **API documentation**: Swagger / OpenAPI UI in Development
- **Dockerized environment**: `docker-compose.yml` with Postgres, Prometheus, Grafana, Jaeger, Seq and the API host

### Tech stack

- **Runtime**: .NET 10
- **Web**: ASP.NET Core 10 Web API (minimal API style)
- **Database**: PostgreSQL (Npgsql provider)
- **ORM**: Entity Framework Core 8
- **Migrations**: EF Core code-first, auto-applied on startup in Development
- **Validation**: FluentValidation
- **Mediation / Application layer**: Handlers per feature (CQRS-style)
- **Logging & tracing**: OpenTelemetry / Seq
- **API Docs**: Swashbuckle (Swagger / OpenAPI)

### Project structure

At a high level, the solution (`ItemsShop.sln`) consists of:

- **Host**
  - `ItemsShop.Host` – ASP.NET Core entrypoint (`Program.cs`), configures logging, infrastructure and maps endpoints.
- **Common**
  - `ItemsShop.Common.Api` – shared API abstractions, error handling, Swagger configuration, health checks.
  - `ItemsShop.Common.Application` – shared enums, application-level helpers.
  - `ItemsShop.Common.Domain` – shared domain primitives and result types.
  - `ItemsShop.Common.Infrastructure` – shared infrastructure (database configuration, OpenTelemetry, etc.).
- **Modules**
  - **Catalogs**
    - `ItemsShop.Catalogs.Domain` – entities for products, categories, carts, cart items.
    - `ItemsShop.Catalogs.Features` – HTTP endpoints and use-cases for catalogs.
    - `ItemsShop.Catalogs.Infrastructure` – EF Core DbContext, migrations and persistence configuration.
    - `ItemsShop.Catalogs.PublicApi` – contracts and module API surface for reuse.
  - **Orders**
    - `ItemsShop.Orders.Domain` – entities for orders and order items.
    - `ItemsShop.Orders.Features` – HTTP endpoints and use-cases for orders.
    - `ItemsShop.Orders.Infrastructure` – EF Core DbContext, migrations and persistence configuration.

Configuration for infrastructure services (PostgreSQL, Prometheus, Grafana, Jaeger, Seq, OpenTelemetry collector) lives under the `Configs/` directory and is wired via `docker-compose.yml`.

### Documentation

The `docs/` directory contains detailed documentation:

- `how-to-run.md` – how to run the project locally (Docker and pure .NET options)
- `making-pull-requests.md` – contributor workflow for creating PRs
- `architecture.md` – high-level architecture and module overview
- `dependencies.md` – external dependencies and how they are used
- `code-style.md` – code style and conventions
- `api-reference.md` – overview of HTTP API endpoints
- `user-guide.md` – how to interact with the API as a consumer
- `development-guide.md` – guidance for local development and extending the system

### How to run

See `docs/how-to-run.md` for step-by-step instructions.

### Educational purpose

This repository is a learning project aimed at exploring technologies such as ASP.NET Core, EF Core, PostgreSQL, OpenTelemetry, containerization, and modular architecture.  
It does not strive for perfect architecture or production-grade code quality; decisions are intentionally optimized for clarity and experimentation.

### License

This project is licensed under the MIT License. See `LICENSE` for details.
