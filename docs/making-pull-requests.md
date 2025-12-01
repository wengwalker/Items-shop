## Development process (making pull requests)

This document describes a recommended workflow for contributing changes via pull requests.  
It complements the high-level guidance in `CONTRIBUTING.md`.

1. **Fork the repository**

   - Click the **Fork** button at the top right of the GitHub repository page.

2. **Clone your fork**

   ```bash
   git clone https://github.com/YOUR_USERNAME/Items-shop.git
   cd Items-shop
   ```

3. **Set upstream remote**

   ```bash
   git remote add upstream https://github.com/wengwalker/Items-shop.git
   ```

4. **Sync with upstream `develop` branch**

   ```bash
   git fetch upstream
   git switch develop
   git pull upstream develop
   ```

5. **Create a feature or bugfix branch from `develop`**

   ```bash
   # feature work
   git switch -c feature/your-feature-name develop

   # or for bug fixes
   git switch -c bugfix/issue-number-description develop
   ```

6. **Make your changes**

   - Keep changes focused and cohesive (one feature or fix per branch).
   - Follow the existing project structure:
     - Domain / Application / Infrastructure / Features per module.
     - Minimal API endpoints implementing `IEndpoint` for HTTP surface.
   - Add or update tests when applicable.
   - Update documentation in `docs/` and `README.md` when behavior or API changes.

7. **Regularly sync with upstream `develop`**

   ```bash
   git fetch upstream
   git rebase upstream/develop
   ```

   Resolve conflicts locally and rerun tests before pushing.

8. **Push your branch**

   ```bash
   git push origin your-branch-name
   ```

9. **Create a pull request targeting `develop`**

   - Navigate to the original repository on GitHub.
   - Click **Compare & pull request**.
   - Ensure the base branch is set to `develop`.
   - Use a PR title that follows the convention described in `CONTRIBUTING.md`.
   - Provide a concise summary, implementation details, and testing notes in the PR description.
   - Link the related issue (for example: “Fixes #123”).

10. **Review and iteration**

    - Address reviewer comments with additional commits (avoid force-push unless asked).
    - Keep the discussion on the PR so future contributors can see the reasoning.

Once the PR is approved and checks pass, a maintainer will merge it into `develop`.
