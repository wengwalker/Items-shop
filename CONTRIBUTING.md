## Contribution Guide

Thank you for your interest in contributing to **Items-shop**! We welcome contributions from the community and are grateful for any help.

### How to Contribute

#### Creating an Issue

**Before starting work** on new functionality or bug fixes:

1. Check existing issues for duplication.
2. Create a new issue with a clear description of the proposed change or problem.
3. Discuss with the maintainers the feasibility and suggested approach.
4. Wait for approval of the idea before starting implementation.

For larger features, please include:

- The problem you are solving.
- A short overview of your proposed solution.
- Any breaking changes or migrations required.

#### Pull Request Process

1. Ensure your branch is created from and targets the `develop` branch.
2. Follow all code standards and guidelines (see `docs/code-style.md`).
3. Update documentation if needed:
   - `README.md` for user-facing changes.
   - `docs/api-reference.md`, `docs/user-guide.md`, or `docs/architecture.md` if the API or flows change.
4. Add or update tests when applicable.
5. Ensure all tests pass locally.
6. Request a review from maintainers.

**PR titles** must match this format: `<type>(<scope>): #<issue> <description>`  
where:
- **type** – change type (for example: `feat`, `fix`, `refac`, `docs`, `chore`).
- **scope** – optional subproject / module (for example: `Catalogs`, `Orders`, `Common`).

Examples:

- `feat(Catalogs): #123 Add product filtering by price`
- `fix: #456 Resolve null reference when creating order`

The title of your Pull Request will become the final commit message on the mainline branch, so a clear and consistent convention is crucial.

#### Branching Strategy

We follow a simple Git branching model:

- `master` – production-ready code.
- `develop` – integration branch for new features and fixes.
- Short-lived feature / bugfix branches created from and merged into `develop`.

**Important:** All feature or bugfix branches must be created from `develop`, not `master`.

Suggested naming:

- `feature/<short-summary>`
- `bugfix/<issue-number>-<short-summary>`

#### Commit messages

- Follow the conventional commits style ([see here](https://www.conventionalcommits.org/en/v1.0.0/)).
- Reference related issues and pull requests in the commit body when useful.

Example:

```text
feat(auth): add user authentication middleware

- Implement JWT token validation
- Add error handling for invalid tokens
- Update API documentation

Fixes #123
```

#### Development Pull Requests Process

See `docs/making-pull-requests.md` for a step-by-step workflow.

#### Documentation

When you change behavior, endpoints or configuration:

- Update `README.md` if the way users run or consume the app changes.
- Update `docs/api-reference.md` when adding or modifying endpoints.
- Update `docs/architecture.md` for structural or architectural changes.
- Update `docs/user-guide.md` and `docs/development-guide.md` when user or developer workflows change.

Keep code samples and configuration snippets in documentation aligned with the actual implementation.

#### Code Standards

- Follow the official [.NET coding conventions](https://learn.microsoft.com/dotnet/csharp/fundamentals/coding-style/coding-conventions).
- Prefer the existing patterns used in the codebase:
  - Minimal API endpoints implementing `IEndpoint`.
  - Feature-per-file structure (Request, Handler, Endpoint, Validator, etc.).
  - Separation into Domain / Application / Infrastructure / Features projects per module.
- Ensure the solution builds without warnings.
- Include tests for new functionality where feasible.
- Update documentation for API changes.

See `docs/code-style.md` and `docs/development-guide.md` for more details.

#### Questions and Support

- For questions, create a GitHub issue with the `question` label.
- Be patient when waiting for responses; this is a volunteer-driven project.
