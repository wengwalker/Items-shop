# Contribution Guide

Thank you for your interest in contributing to Items Shop! We welcome contributions from the community and are grateful for any help.

## Ways to Contribute

There are several ways you can contribute to the project:

1. **Report Bugs**: Submit detailed bug reports through [GitHub Issues](https://github.com/wengwalker/Items-shop/issues)
2. **Suggest Features**: Propose new features or enhancements
3. **Fix Issues**: Help resolve open issues
4. **Improve Documentation**: Enhance existing docs or create new ones

## Creating an Issue

**Before starting work** on new functionality or bug fixes, you must:

1. Check existing issues for duplication
2. Create a new issue with a description of the proposed change
3. Discuss with the maintainers the feasibility and approach to implementation
4. Wait for approval of the idea before starting work

## Pull Request Process

1. Ensure your branch is created from and targets the develop branch
2. Follow all code standards and guidelines
3. Update documentation if needed
4. Add tests if applicable
5. Ensure all tests pass
6. Request review from maintainers

## Branching Strategy

We follow a Git branching model where:

- `master` branch contains production-ready code
- `develop` branch is used for integration of new features or resolve bugfixes
- Feature and Bugfix branches are created from and merged into `develop`

**Important**: All feature or bugfix branches must be created from the `develop` branch, not from `master`.

## Commit messages

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

## Development Process

See in `docs/making-pull-requests.md`

## Documentation

- Update README.md if you change interface of usage
- Document new API endpoints or parameters
- Add comments for complex code sections
- Keep code examples in documentation current

## Questions and Support

- For questions, create a GitHub Issue with the "question" label
- Please be patient for responses as this is a volunteer project
