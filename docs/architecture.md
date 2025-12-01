## Architecture

This document describes the high-level architecture of **Items-shop**: solution structure, modules, and cross-cutting concerns.

### High-level overview

- **Type of system**: HTTP JSON API for a simple marketplace domain.
- **Runtime**: ASP.NET Core 10 Web API, using minimal-API-style endpoint classes.
- **Persistence**: PostgreSQL, one logical database used by multiple bounded contexts / modules.
- **Observability**: OpenTelemetry for logs, traces, and metrics, scraped and visualized via Prometheus, Grafana, Jaeger, Seq.
- **Infrastructure**: Docker Compose with Postgres, observability stack, and the API host.

The solution follows a modular, layered approach:

- A single **Host** project boots ASP.NET Core, wiring shared infrastructure and modules.
- **Common** projects provide reusable primitives across modules.
- Each domain module (Catalogs, Orders) is split into **Domain**, **Application / Features**, and **Infrastructure** projects.

---

### Solution layout

Projects are defined in `ItemsShop.sln` and grouped as follows:

- **Host**
  - `ItemsShop.Host`
    - ASP.NET Core entrypoint (`Program.cs`).
    - Configures logging, OpenTelemetry, health checks, Swagger, and module registration.
    - Calls `MapEndpoints()` to register all minimal API endpoints discovered via DI.

- **Common**
  - `ItemsShop.Common.Api`
    - Defines `IEndpoint` abstraction used for all HTTP endpoints.
    - Configures Swagger, ProblemDetails error handling, and endpoint discovery via `MapEndpoints`.
  - `ItemsShop.Common.Application`
    - Shared enums (for example, query sort types) and application-level helpers.
  - `ItemsShop.Common.Domain`
    - Cross-cutting domain primitives and result types (success/failure with error metadata).
  - `ItemsShop.Common.Infrastructure`
    - Shared infrastructure concerns (database configuration, OpenTelemetry integration, etc.).

- **Modules**
  - **Catalogs**
    - `ItemsShop.Catalogs.Domain`
      - Entities for products, categories, carts, and cart items.
    - `ItemsShop.Catalogs.Features`
      - Feature-oriented endpoints (Products, Categories, Carts, CartItems).
      - Each feature typically has Request, Handler, Endpoint, and Validator classes.
    - `ItemsShop.Catalogs.Infrastructure`
      - EF Core DbContext, entity configurations, migrations, and persistence wiring.
    - `ItemsShop.Catalogs.PublicApi`
      - Contracts and public-facing API surface for reusing in other modules.
  - **Orders**
    - `ItemsShop.Orders.Domain`
      - Entities for orders and order items.
    - `ItemsShop.Orders.Features`
      - HTTP endpoints for orders and order items, plus related application logic.
    - `ItemsShop.Orders.Infrastructure`
      - EF Core DbContext, entity configurations, migrations, and persistence wiring.

Configuration for infrastructure services (Postgres, OTel collector, Prometheus, Grafana, Jaeger, Seq) lives under `Configs/` and is wired into `docker-compose.yml`.

---

### Layering and responsibilities

Each module roughly follows a layered approach:

- **Domain layer**
  - Pure domain entities, value objects and invariants.
  - No external dependencies (no EF Core, no ASP.NET types).

- **Application / Features layer**
  - Use cases and orchestration logic.
  - Handlers processing requests (for example, “GetProducts”, “CreateOrder”).
  - Validation using FluentValidation.
  - Minimal API endpoint classes implementing `IEndpoint`, mapping HTTP routes to handlers.

- **Infrastructure layer**
  - EF Core DbContext and entity type configurations.
  - Migrations, repository abstractions, and any external integrations.
  - Database connection is configured via `ConnectionStrings:Postgres` and shared across modules.

The **Host** project orchestrates all of the above:

- Configures logging and OpenTelemetry.
- Registers shared services from `Common` projects.
- Registers modules (Catalogs and Orders) via their `DependencyInjection` entrypoints.
- Ensures databases are migrated at startup in Development.
- Maps health check and feature endpoints.

---

### Endpoint model

All HTTP endpoints implement the `IEndpoint` interface from `ItemsShop.Common.Api`:

- Each endpoint class:
  - Implements `void MapEndpoint(IEndpointRouteBuilder builder)`.
  - Registers its own route, HTTP method, OpenAPI metadata, and error responses.
  - Delegates to an application handler and returns standardized results.

The host calls `app.MapEndpoints()` which:

- Discovers all registered `IEndpoint` services via dependency injection.
- Invokes `MapEndpoint` on each of them to register routes.

This pattern keeps the HTTP surface modular and feature-oriented, rather than aggregating everything into large controller classes.

---

### Cross-cutting concerns

**Error handling**

- Centralized error handling is configured via ProblemDetails and a global exception handler.
- Validation failures and domain/application errors are mapped to standardized HTTP responses.

**Logging and observability**

- Logging is configured via `AddCoreHostLogging`, which:
  - Clears default logging providers.
  - Configures console logging.
  - Sets up OpenTelemetry logging with OTLP exporter.
- Traces and metrics are exported to the OpenTelemetry collector container and then to Prometheus / Jaeger / Grafana.

**Health checks**

- Health checks are mapped via `MapHealthChecksEndpoints` in the host.
- `docker-compose.yml` configures container health checks that call the API’s health endpoint.

---

### Data model (high-level)

At a conceptual level:

- **Catalogs**
  - **Product**
    - Belongs to a **Category**.
    - Has price, description, quantity and other basic fields.
  - **Category**
    - Groups products.
  - **Cart**
    - Represents a shopping cart, potentially associated with a user.
  - **CartItem**
    - Product, quantity, and pricing information in a specific cart.

- **Orders**
  - **Order**
    - Represents a finalized purchase, typically created from a cart.
    - Contains order metadata (status, total price, etc.).
  - **OrderItem**
    - Materializes items from a cart at order time.
    - Keeps product, quantity and price snapshot at the time of ordering.

Exact schemas and relationships are implemented in the Domain and Infrastructure projects; see the EF Core DbContexts or Swagger models for full detail.

---

### Extensibility

To add new functionality:

- Add or extend entities in the appropriate Domain project.
- Implement handlers and endpoints in the corresponding Features project.
- Update DbContext and migrations in the Infrastructure project if persistence changes.
- Wire new services via the module’s dependency injection configuration.
- Update documentation (`docs/api-reference.md`, `docs/user-guide.md`, `docs/development-guide.md`) accordingly.

See `docs/development-guide.md` for concrete development workflows.
