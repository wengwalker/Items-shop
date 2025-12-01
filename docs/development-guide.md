## Development guide

This guide is intended for developers who want to work on **Items-shop**, extend its functionality, or understand its internals.

### Prerequisites

- .NET 10 SDK (`10.x`)
- Docker Desktop (if you want to use the full dockerized environment)
- PostgreSQL 14+ (if running without Docker)

Make sure you have cloned the repository and can build the solution:

```bash
cd src
dotnet restore ItemsShop.sln
dotnet build ItemsShop.sln
```

---

### Running the application for development

See `docs/how-to-run.md` for detailed instructions on:

- Running with Docker (recommended for full observability stack).
- Running locally without Docker (using your own Postgres instance).

For development, the usual workflow is:

1. Start the application (Docker or local).
2. Make code changes in the module you’re working on.
3. Rebuild and run tests.
4. Exercise the changes via Swagger, Postman, or automated tests.

---

### Solution structure (recap)

High-level project groups:

- `Host/ItemsShop.Host` – ASP.NET Core entrypoint and composition root.
- `Common/*` – shared API, domain, application and infrastructure components.
- `Catalogs/*` – Catalogs module (Domain, Features, Infrastructure, PublicApi).
- `Orders/*` – Orders module (Domain, Features, Infrastructure).

See `docs/architecture.md` for a more detailed view.

---

### Working on a feature

Typical steps when adding or modifying a feature:

1. **Pick the module**
   - Catalog-related features → `Catalogs` projects.
   - Order-related features → `Orders` projects.
2. **Design the domain change**
   - Update or create entities in the corresponding `*.Domain` project if necessary.
3. **Add application logic**
   - Create or modify handlers in the `*.Features` project.
   - Keep the feature self-contained in its folder (Request, Handler, Endpoint, Validator, etc.).
4. **Expose or update HTTP endpoints**
   - Implement or adjust an `IEndpoint` class in the `Features` project:
     - Map the desired route under `/api/v1/...`.
     - Wire up OpenAPI metadata via `WithSummary`, `WithDescription`, `.Produces*`.
5. **Update persistence**
   - Modify the DbContext, configurations or migrations in the `*.Infrastructure` project if the data model changes.
6. **Update documentation**
   - `docs/api-reference.md` for new or changed endpoints.
   - `docs/user-guide.md` for user-facing behavior change.
   - `docs/architecture.md` if structural changes are significant.

Be sure to follow the patterns already used in the module; they are your primary reference.

---

### Database migrations

EF Core migrations live in the Infrastructure projects for each module.

#### Adding a migration (example for Catalogs)

1. Install EF Core tools (if you haven’t already):

   ```bash
   dotnet tool install --global dotnet-ef
   ```

2. From the `src/` directory, go to the Catalogs infrastructure project:

   ```bash
   cd src/Catalogs/ItemsShop.Catalogs.Infrastructure
   ```

3. Add a migration (replace `<Name>` with a meaningful name, and replace `<Context>` to specified DbContext):

   ```bash
   dotnet ef migrations add <Name> -p ItemsShop.Catalogs.Infrastructure.csproj -s ../../Host/ItemsShop.Host/ItemsShop.Host.csproj -c <Context> -v
   ```

4. Verify the generated migration and apply it by running the application (in Development, migrations are applied automatically on startup).

Repeat similarly for the Orders module using `ItemsShop.Orders.Infrastructure`.

> **Important**  
> Do not manually modify migration code unless you know exactly what you are doing.

---

### Observability and diagnostics

When running with Docker (`docker-compose.yml`):

- Logs:
  - View structured logs in **Seq** at `http://localhost:8081`.
- Metrics:
  - Explore metrics in **Prometheus** at `http://localhost:9090`.
  - Visualize dashboards in **Grafana** at `http://localhost:3000`.
- Traces:
  - Inspect distributed traces in **Jaeger** at `http://localhost:16686`.

This stack is very useful when debugging performance issues, failures, or understanding the behavior of new features.

---

### Coding standards & patterns

Refer to `docs/code-style.md` for details, but in short:

- Keep feature code localized (feature folders with Request, Handler, Endpoint, Validator, etc.).
- Use minimal APIs and `IEndpoint` implementations instead of large controllers.
- Use FluentValidation for request validation.
- Use shared result types to propagate success/failure from handlers to endpoints.
- Prefer composition via dependency injection over static helpers or service locators.

---

### Testing

If test projects are present:

- Keep tests close to the feature or behavior under test.
- Use descriptive names and clear Arrange-Act-Assert structure.
- Test:
  - Domain logic (pure business rules) without infrastructure.
  - Application logic and handlers with mocks/fakes where necessary.
  - Integration-level behaviors (optional), for example via in-memory or test-specific databases.

When adding new features, consider adding tests to cover the most important paths and failure modes.

---

### Contribution workflow

See:

- `CONTRIBUTING.md` – contribution rules and expectations.
- `docs/making-pull-requests.md` – step-by-step PR workflow.

In summary:

- Create an issue and discuss the change before starting substantial work.
- Work on short-lived branches off `develop`.
- Submit PRs that are focused, tested, and documented.

Following these guidelines will make it easier to review and merge your changes.
