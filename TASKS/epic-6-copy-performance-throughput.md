# Epic 6: Copy Performance & Throughput
1. Story: Establish baseline and performance targets.
1. Task: Define repeatable benchmark datasets (small, medium, large) with mixed RAW/video files.
1. Task: Measure current copy duration, throughput, and CPU/disk usage profile.
1. Task: Document explicit target goals (time reduction and throughput increase).

1. Story: Research optimization options.
1. Task: Evaluate bounded multi-threading with configurable max degree of parallelism.
1. Task: Evaluate stream-level optimizations (buffer sizing, async file IO path) for large files.
1. Task: Evaluate preflight optimizations (destination existence checks, directory prep, extension routing hot path).
1. Task: Record tradeoffs for SSD vs removable card media and recommend a default strategy.

1. Story: Implement bounded parallel copy pipeline.
1. Task: Introduce a copy worker model with bounded concurrency and deterministic summary aggregation.
1. Task: Preserve existing behaviors (no overwrite, RAW/video routing, image skip, per-file failures).
1. Task: Add configuration setting for max parallel copy workers and safe default.

1. Story: Add performance observability.
1. Task: Capture and report elapsed time and throughput in completion summary.
1. Task: Add lightweight instrumentation logs around preflight, copy, and summary phases.

1. Story: Verify correctness and gains.
1. Task: Add regression tests for routing/skip/failure behavior under parallel copy.
1. Task: Re-run baseline benchmarks and compare results against target goals.
1. Task: Document final recommendation and any environment-specific tuning notes.
