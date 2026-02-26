# Epic 5: Spec Alignment & UX Feedback
1. Story: Folder structure matches documented standard.
1. Task: Reconcile PRD folder structure with implementation and update UI labels as needed.
1. Task: Add check to ensure folder creation matches PRD structure.

1. Story: Clear per-file error feedback.
1. Task: Capture per-file copy failures with filename and error reason.
1. Task: Provide summary counts for copied, skipped, and failed files.

1. Story: Copy execution stability.
1. Task: Prevent collection-modified exceptions by snapshotting selected files before background copy starts.
1. Task: Prevent concurrent Generate runs by guarding against duplicate background worker execution.

1. Story: Output subfolder naming alignment.
1. Task: Ensure output subfolder naming is consistently `LRC` across copy logic, folder creation checks, and documentation.
