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

## How to run

See in `docs/how-to-run.md`

## Educational purpose

This repository is a learning project aimed at exploring technologies (ASP.NET Core, EF Core, PostgreSQL, Serilog, validation/mediator patterns, containerization, etc.). It does not strive for perfect architecture or production-grade code quality. Decisions are intentionally optimized for clarity and experimentation rather than completeness or robustness.

## License

This project is licensed under the MIT License. See `LICENSE` for details.
