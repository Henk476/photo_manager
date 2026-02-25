# Tasks

## Epic 1: Configuration & Defaults
1. Story: Configure default input drive.
1. Task: Define settings storage approach (e.g., app settings file) and schema for default input drive.
1. Task: Implement load/save for default input drive setting.
1. Task: Update file picker to use configured default input drive with fallbacks.
1. Task: Add UI to edit default input drive.
1. Task: Validate behavior when drive is missing.

1. Story: Configure default output root.
1. Task: Define settings field for default output root.
1. Task: Implement load/save for default output root setting.
1. Task: Update output path generation to use default output root.
1. Task: Add UI to edit default output root.
1. Task: Validate behavior when output root is invalid.

1. Story: Configure folder template.
1. Task: Define settings field for folder template.
1. Task: Implement load/save for folder template setting.
1. Task: Implement template parsing with allowed tokens.
1. Task: Add UI to edit folder template and validation feedback.
1. Task: Update output path generation to use the template.

## Epic 2: Ingest & Selection
1. Story: Default to removable memory card.
1. Task: Detect removable drives and determine preferred removable drive.
1. Task: Implement fallback to configured default input drive or last-used folder.
1. Task: Wire picker initial directory to detection logic.

1. Story: Single filter for RAW and video.
1. Task: Ensure picker filter matches PRD list for RAW and video formats.
1. Task: Add test selection coverage for mixed-format multi-select.

## Epic 3: Copy & Organization
1. Story: RAW formats copy to `RAW`.
1. Task: Expand copy logic to include all RAW extensions.
1. Task: Normalize extension handling to be case-insensitive.
1. Task: Add verification of skip-on-existing behavior for RAW.

1. Story: Video formats copy to output root.
1. Task: Expand copy logic to include video extensions.
1. Task: Normalize extension handling to be case-insensitive.
1. Task: Add verification of skip-on-existing behavior for video.

1. Story: Ignore image formats.
1. Task: Ensure image extensions are skipped during copy.
1. Task: Confirm no warnings by default for skipped images.

1. Story: Ensure subfolders exist.
1. Task: Update folder creation to always ensure `RAW`, `Edit`, `Select` exist.
1. Task: Verify idempotent creation when folders already exist.

## Epic 4: Output Path Management
1. Story: Browse button updates output path.
1. Task: Fix browse flow so selected folder updates output path field.
1. Task: Ensure selected path is used by copy logic.

1. Story: Manual edits are respected and validated.
1. Task: Detect manual edits and use them during copy.
1. Task: Validate path before copy with clear error message.
1. Task: Prevent project name change from overwriting manual output path unless user requests regeneration.

## Epic 5: Spec Alignment & UX Feedback
1. Story: Folder structure matches documented standard.
1. Task: Reconcile PRD folder structure with implementation and update UI labels as needed.
1. Task: Add check to ensure folder creation matches PRD structure.

1. Story: Clear per-file error feedback.
1. Task: Capture per-file copy failures with filename and error reason.
1. Task: Provide summary counts for copied, skipped, and failed files.
