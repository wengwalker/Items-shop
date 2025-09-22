# Development process (making pull requests)

1. **Fork the Repository**

   - Click the "Fork" button at the top right of the repository page

2. **Clone your Fork**

```bash
git clone https://github.com/YOUR_USERNAME/Items-shop.git
cd Items-shop
```

3. **Set Upstream Remote**

```bash
git remote add upstream https://github.com/wengwalker/Items-shop.git
```

4. **Sync with Upstream Develop branch**

```bash
git fetch upstream
git switch develop
git pull upstream develop
```

5. **Create a Feature or Bugfix branch from develop**

```bash
git switch -c feature/your-feature-name develop
# or for bug fixes:
git switch -c bugfix/issue-number-description develop
```

6. **Make your changes**

- Follow the code standards below
- Add tests if applicable
- Update documentation as needed

7. **Regularly Sync with Upstream Develop**

```bash
git fetch upstream
git rebase upstream/develop
```

8. **Push changes**

```bash
git push origin your-branch-name
```

9. **Create a Pull Request to Develop branch**

- Navigate to the original repository
- Click "Compare & pull request"
- Ensure the base branch is set to `develop`
- Fill out the PR template
