# Contribution Guide

Thank you for your interest in contributing to Items Shop! We welcome contributions from the community and are grateful for any help.

## How to Contribute

### Creating an Issue

**Before starting work** on new functionality or bug fixes, you must:

1. Check existing issues for duplication
2. Create a new issue with a description of the proposed change
3. Discuss with the maintainers the feasibility and approach to implementation
4. Wait for approval of the idea before starting work

**Important for features:** Check if your idea fits the project's scope and isn't already discussed. Provide detailed description of the proposed change.

### Pull Request Process

1. Ensure your branch is created from and targets the develop branch
2. Follow all code standards and guidelines
3. Update documentation if needed
4. Add tests if applicable
5. Ensure all tests pass
6. Request review from maintainers

**Important:** The names of the Pull Requests must match this format: `<Type>(<Scope>): #<Issue> <Description>`, where `type` means the type of changes, `scope` means the subproject/subsystem where the changes are being made (optional), for example: `feat(Auth): #123 Add OAuth2 support` or `fix: #456 Resolve null reference in login`

**Important:** This project follows a merge strategy. The title of your Pull Request (PR) will become the final commit message in the main branch. Therefore, following a consistent naming convention is crucial for clarity and generating change logs

### Branching Strategy

We follow a Git branching model where:

- `master` branch contains production-ready code
- `develop` branch is used for integration of new features or resolve bugfixes
- Feature and Bugfix branches are created from and merged into `develop`

**Important:** All feature or bugfix branches must be created from the `develop` branch, not from `master`.

### Commit messages

- Follow the commit convention ([see here](https://www.conventionalcommits.org/en/v1.0.0/))
- Reference issues and pull requests liberally
- Example:

```text
Add user authentication middleware

- Implement JWT token validation
- Add error handling for invalid tokens
- Update API documentation

Fixes #123
```

### Development Pull Requests Process

See in `docs/making-pull-requests.md`

### Documentation

- Update README.md if you change interface of usage
- Document new API endpoints or parameters
- Add comments for complex code sections
- Keep code examples in documentation current

### Code Standards

- Follow [.NET Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- Ensure code builds without warnings
- Include tests for new functionality
- Update documentation for API changes

### Questions and Support

- For questions, create a GitHub Issue with the "question" label
- Please be patient for responses as this is a volunteer project
