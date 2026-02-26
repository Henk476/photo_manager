# Epic 7: Copy Progress Visibility
1. Story: Show real-time copy progress.
1. Task: Add progress state model (total, processed, copied, skipped, failed).
1. Task: Add UI controls for progress display (progress bar + status labels).
1. Task: Emit progress updates from copy pipeline at file-level granularity.
1. Task: Marshal progress updates safely to UI thread.

1. Story: Surface current file activity.
1. Task: Display current filename while processing.
1. Task: Clear current filename and show completion status when done.

1. Story: Add elapsed and ETA feedback.
1. Task: Track elapsed copy time with a stopwatch.
1. Task: Compute and display ETA after initial sample threshold.
1. Task: Handle low-confidence ETA cases with fallback messaging.

1. Story: Verify progress correctness.
1. Task: Ensure progress totals match final summary totals.
1. Task: Add regression tests for mixed outcomes (copied/skipped/failed) and progress consistency.
1. Task: Validate UI responsiveness during long copy runs.
