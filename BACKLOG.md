# Backlog

## Epic 1: Configuration & Defaults
1. Story: Configure default input drive.
1. Acceptance Criteria:
1. A configurable setting exists for default input drive.
1. When set, file picker opens to that drive if present.
1. If not present, app falls back to the best available removable drive; otherwise, last-used folder.

1. Story: Configure default output root.
1. Acceptance Criteria:
1. A configurable setting exists for default output root.
1. Output path generation uses this root by default.
1. If the root is invalid/unavailable, the app shows a clear error and does not copy.

1. Story: Configure folder template.
1. Acceptance Criteria:
1. A configurable folder template accepts `{YYYY}`, `{MM}`, `{DD}`, `{MMMM}`, and `{Project}`.
1. Output path generation respects the template.
1. Invalid tokens are rejected with a clear validation message.

## Epic 2: Ingest & Selection
1. Story: Default to removable memory card.
1. Acceptance Criteria:
1. On open, the file picker targets the first available removable drive.
1. If multiple removable drives exist, the most recently used removable drive is preferred.
1. If no removable drives exist, it uses the configured default input drive (if valid) or last-used folder.

1. Story: Single filter for RAW and video.
1. Acceptance Criteria:
1. File picker includes all RAW and video formats specified in the PRD.
1. User can multi-select files across the supported formats in one picker session.

## Epic 3: Copy & Organization
1. Story: RAW formats copy to `RAW`.
1. Acceptance Criteria:
1. RAW formats (`.nef .cr2 .cr3 .arw .dng .raf .orf .rw2 .pef .srw`) are copied to `RAW`.
1. Existing files are not overwritten.
1. Copy is case-insensitive on extensions.

1. Story: Video formats copy to output root.
1. Acceptance Criteria:
1. Video formats (`.mp4 .mov .avi .mxf`) are copied to output root.
1. Existing files are not overwritten.
1. Copy is case-insensitive on extensions.

1. Story: Ignore image formats.
1. Acceptance Criteria:
1. Image formats (`.jpg .jpeg .png .tif .tiff`) are skipped during copy.
1. Skipped images do not cause errors or warnings by default.

1. Story: Ensure subfolders exist.
1. Acceptance Criteria:
1. `RAW`, `Edit`, `LRC` are created if missing, even when output root exists.
1. Folder creation is idempotent and does not error if folders already exist.

## Epic 4: Output Path Management
1. Story: Browse button updates output path.
1. Acceptance Criteria:
1. Selecting a folder updates the output path field to the selected location.
1. The selected path is used for subsequent copy operations.

1. Story: Manual edits are respected and validated.
1. Acceptance Criteria:
1. If the user edits the output path field, the app uses that path for copy.
1. Invalid paths are detected before copy with a clear error message.
1. Project name changes do not override a manually edited output path unless the user requests regeneration.

## Epic 5: Spec Alignment & UX Feedback
1. Story: Folder structure matches documented standard.
1. Acceptance Criteria:
1. The folder structure created by the app matches the PRD exactly.
1. Any changes to the structure are reflected in the PRD and the UI labels.

1. Story: Clear per-file error feedback.
1. Acceptance Criteria:
1. If a file fails to copy, the user is shown the filename and error reason.
1. The summary includes total files copied, skipped, and failed.

1. Story: Copy execution stability.
1. Acceptance Criteria:
1. Copy uses a stable snapshot of selected source files and does not fail from UI-triggered source list changes during execution.
1. While a copy run is active, the Generate action is disabled or ignored to prevent concurrent runs.

1. Story: Output subfolder naming alignment.
1. Acceptance Criteria:
1. Output folder creation and verification use `RAW`, `Edit`, and `LRC`.
1. References to `Select` as an output subfolder are removed from active specs and task docs.
