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

## Epic 6: Copy Performance & Throughput
1. Story: Establish baseline and performance targets.
1. Acceptance Criteria:
1. A repeatable benchmark scenario is defined (small, medium, large copy sets with mixed RAW/video files).
1. Baseline copy throughput and total duration are measured on current implementation.
1. A target improvement is documented (for example, at least 2x faster on medium/large sets where IO allows).

1. Story: Research and compare optimization strategies.
1. Acceptance Criteria:
1. At least three approaches are compared with notes on tradeoffs: bounded multi-threading, async/stream tuning, and file-system preflight optimizations.
1. A recommended default strategy is selected with a fallback strategy for slower/removable media.
1. The chosen strategy includes guardrails for reliability (no overwrite, consistent error handling, stable summary results).

1. Story: Implement bounded parallel copy pipeline.
1. Acceptance Criteria:
1. Copy execution supports configurable max parallelism (default safe value, e.g. 2-4 workers).
1. Throughput improves versus baseline for medium/large sets without breaking existing copy rules.
1. UI remains responsive and prevents duplicate runs while processing.

1. Story: Add observability and verification for performance.
1. Acceptance Criteria:
1. Copy summary includes elapsed time and effective throughput (files/sec or MB/sec).
1. Regression checks validate functional correctness (routing, skip-on-existing, error reporting) under parallel execution.
1. Benchmark rerun after implementation demonstrates improvement against baseline.

## Epic 7: Copy Progress Visibility
1. Story: Show real-time copy progress.
1. Acceptance Criteria:
1. The UI shows progress during copy as both percentage and counts (processed/total).
1. Progress updates occur continuously without freezing the UI.
1. Progress accurately reflects copied, skipped, and failed outcomes.

1. Story: Surface current file activity.
1. Acceptance Criteria:
1. The UI shows the currently processing filename (or short path) while copy is running.
1. On completion, current-file indicator clears and final status is shown.

1. Story: Improve long-run user feedback.
1. Acceptance Criteria:
1. The UI displays elapsed time and estimated remaining time when enough samples exist.
1. Status messaging remains clear for large batches and removable media.

1. Story: Preserve correctness and stability with progress reporting.
1. Acceptance Criteria:
1. Existing copy rules (routing, skip-on-existing, no-overwrite, error capture) are unchanged.
1. Progress reporting remains correct under sequential and parallel copy modes.
1. Automated checks verify progress events and final totals are consistent.
