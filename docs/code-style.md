## Code style and conventions

This document describes the coding style and structural conventions used in **Items-shop**.

> **Goal**  
> Keep the codebase consistent and easy to navigate, even as multiple contributors work on different modules.

### General C# conventions

- Follow the official [.NET C# coding conventions](https://learn.microsoft.com/dotnet/csharp/fundamentals/coding-style/coding-conventions).
- Prefer:
  - `var` for local variables when the type is obvious from the right-hand side.
  - Expression-bodied members for simple methods and properties where it improves readability.
  - `readonly` fields and `init`-only properties where applicable.
- Use `async`/`await` for I/O-bound operations; avoid blocking calls (`.Result`, `.Wait()`).

### Project and namespace organization

- Namespaces mirror the physical folder structure, for example:
  - `ItemsShop.Catalogs.Features.Features.Products.GetProducts`
  - `ItemsShop.Orders.Features.Features.Orders.CreateOrder`
- Group files by feature rather than by technical type:
  - For a given feature, keep `Request`, `Handler`, `Endpoint`, `Validator` etc. in the same folder.
- Shared constants and cross-feature helpers live in `Shared/` inside each module (for example, `Shared/Consts`).

### Endpoint pattern (`IEndpoint`)

HTTP endpoints are implemented as classes that implement `IEndpoint`:

- Each endpoint is a small class responsible for:
  - Declaring the route, HTTP verb, and OpenAPI metadata in `MapEndpoint`.
  - Delegating to an application handler that performs the real work.
  - Translating domain/application results to HTTP responses.
- Conventions:
  - File name: `<Feature>.Endpoint.cs` (for example, `GetProducts.Endpoint.cs`).
  - Class name: `<Feature>Endpoint` (for example, `GetProductsEndpoint`).
  - Namespace: end with `...Features.<Area>.<Feature>` as in the existing code.
  - Use `WithSummary`, `WithDescription`, `.Produces<>()`, `.ProducesProblem()` and `.ProducesValidationProblem()` to keep Swagger accurate.

### Request / response models

- Use C# `record` types for request and response DTOs when appropriate.
- Keep HTTP-facing DTOs in the Features or PublicApi projects (not in Domain).
- Avoid leaking EF Core entities or internal domain types directly to the API surface.

### Validation

- Use FluentValidation for request validation.
- Each request type should have a dedicated validator class when constraints are non-trivial.
- Keep validation close to the feature (same folder as the request/handler/endpoint).
- Return validation failures as standardized ProblemDetails (this is already wired via shared extensions).

### Error handling and results

- Use the shared result types in `ItemsShop.Common.Domain` to represent success/failure in handlers.
- Handlers should:
  - Return success with strongly-typed values.
  - Return failure with an error code and description where relevant.
- Endpoints are responsible for translating results into HTTP responses:
  - `200 OK` / `201 Created` for success.
  - `400 Bad Request` with validation problems.
  - `404 Not Found` where relevant.
  - Other codes as indicated by the domain error.

### Dependency injection and configuration

- Register module services (DbContexts, handlers, endpoints) via the module’s `DependencyInjection` extension.
- Keep DI registration close to the module; avoid configuring module services in the host.
- Use constructor injection for dependencies.
- Avoid static service locators.

### Naming

- **Classes**: PascalCase (for example, `CreateOrderHandler`, `GetCartItemsEndpoint`).
- **Interfaces**: PascalCase prefixed with `I` (for example, `IGetProductsHandler`).
- **Methods**: PascalCase (for example, `HandleAsync`, `MapEndpoint`).
- **Parameters and locals**: camelCase.
- **Constants**: PascalCase for route constants and domain constants (consistent with current code).

### Comments and documentation

- Prefer self-explanatory code and names; use comments for:
  - Non-obvious business rules.
  - Workarounds or limitations tied to external systems.
  - Rationale behind unusual architectural decisions.
- Keep XML documentation comments for public APIs where they add value, but avoid noise.

### Tests (if present)

- Follow similar feature-based organization in test projects:
  - Mirror the structure of Features and Domain where possible.
- Use clear Arrange-Act-Assert structure in tests.
- Keep test names descriptive (`MethodName_ShouldDoSomething_WhenCondition`).

When contributing new code, follow existing patterns in the closest module/feature as a reference, and update this document if you introduce a new pattern that should be reused.
