# Epic 3: Copy & Organization
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
1. Task: Update folder creation to always ensure `RAW`, `Edit`, `LRC` exist.
1. Task: Verify idempotent creation when folders already exist.
