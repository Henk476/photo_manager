# Epic 8: Theme Support & Windows Sync
1. Story: Introduce app theme system.
1. Task: Define a theme model (`System`, `Light`, `Dark`) and shared theme palette mappings.
1. Task: Implement centralized theme application helper for form and child controls.
1. Task: Apply theme helper to main form and settings form.

1. Story: Default to current Windows theme.
1. Task: Add settings value for theme preference with default `System`.
1. Task: Detect current Windows app theme (light/dark) on startup.
1. Task: Add safe fallback to `Light` when system theme cannot be read.

1. Story: Add user theme preference in settings.
1. Task: Add theme selector control in settings UI with `System`, `Light`, `Dark`.
1. Task: Persist selected theme and apply it after save.
1. Task: Ensure override behavior works when user picks `Light` or `Dark`.

1. Story: Verify consistency and accessibility.
1. Task: Validate readability/contrast for labels, text boxes, buttons, progress, and status labels in all themes.
1. Task: Verify no workflow regressions with theme changes (load source, generate copy, progress updates, settings save).
1. Task: Document theme behavior in project docs.
