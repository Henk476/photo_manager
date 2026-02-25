# Product Requirements Document (PRD)
Project: Canute Photo Organizer
Date: 2026-02-24
Owner: (TBD)

## Overview
Canute Photo Organizer is a Windows Forms desktop app that helps a photographer quickly organize photos from a shoot by copying selected files into a standardized folder structure. It is optimized for manual selection of images and an opinionated target directory layout on the `F:\` drive.

## Problem Statement
After a shoot, photographers need a fast, repeatable way to move RAW and JPG files into a consistent folder structure without manually creating folders and copying files in Explorer.

## Goals
- Reduce time to organize files for a new project.
- Enforce a consistent folder structure (`RAW`, `Edit`, `LRC`).
- Allow the user to select a project name and set an output path.
- Avoid overwriting existing files.

## Non‑Goals
- Automatic ingest from camera/card.
- Editing or metadata changes.
- File renaming, deduplication, or format conversion.
- Cloud sync or backup.
- Cross‑platform support (Windows only).

## Target Users
- A single photographer (or small team) using Windows with a fixed drive layout.

## Key User Flow
1. User opens the app.
2. User enters a project name (optional).
3. User selects source files via file picker (multi‑select).
4. App displays selected filenames and count.
5. User optionally changes output folder.
6. User clicks Generate.
7. App creates target folders and copies files.
8. App opens File Explorer to the output folder.

## Functional Requirements
1. Project name input:
   - Optional text field.
   - If provided, output path is `G:\Work\{YYYY}-{MM}-{DD} - {Project}`.
   - If not provided, output path is `G:\Work\{YYYY}-{MM}-{DD}`.
2. Source file selection:
   - File picker defaults to the memory card (removable drive) when available.
   - Supports multi‑select.
   - Filters to `.png`, `.jpg`, `.jpeg`, `.tif`, `.tiff`, `.nef`, `.cr2`, `.cr3`, `.arw`, `.dng`, `.raf`, `.orf`, `.rw2`, `.pef`, `.srw`, `.mp4`, `.mov`, `.avi`, `.mxf`.
   - Selected filenames are shown in a read‑only list.
3. Output path:
   - Default is `G:\Work\{YYYY}-{MM}-{DD}`.
   - User can browse for a custom folder.
   - Output path is shown in a text field.
4. Configuration:
   - Provide a configuration option for default input drive (used by the file picker).
   - Provide a configuration option for default output root (e.g., `G:\Work`).
   - Provide a configuration option for folder template (e.g., `{YYYY}-{MM}-{DD} - {Project}`).
5. Copy behavior:
   - Create output folder structure if missing:
     - `RAW`, `Edit`, `Select`.
   - Copy RAW formats (`.nef`, `.cr2`, `.cr3`, `.arw`, `.dng`, `.raf`, `.orf`, `.rw2`, `.pef`, `.srw`) to `RAW`.
   - Copy video formats (`.mp4`, `.mov`, `.avi`, `.mxf`) to root of output path.
   - Ignore image formats (`.jpg`, `.jpeg`, `.png`, `.tif`, `.tiff`) during copy.
   - Do not overwrite existing files.
6. Completion:
   - Open File Explorer to the output folder.
7. Clear:
   - Clear selected source list and reset count.
8. Exit:
   - Close the app immediately.

## UX Requirements
- Single‑window, minimal controls.
- `Generate` should be the default action (Enter key).
- Display file count.
- Basic error messages on IO exceptions.

## Data & Storage
- No database.
- Reads from user‑selected files.
- Writes only to the output directory.

## Edge Cases & Error Handling
- Output drive missing or not writable.
- Source file no longer exists.
- Duplicate file names in destination (skip copy).
- Unsupported extensions in selection (ignore).
- Output path text field edited manually (should remain consistent).

## Technical Constraints
- Windows only (WinForms).
- .NET 8.0 Windows Desktop.
- Assumes `F:\` and `G:\` drive letters exist by default.

## Success Metrics
- Time to organize a shoot from selection to output folder is under 1 minute.
- Zero manual folder creation needed for standard cases.
- No file overwrite accidents.

## Risks
- Hard‑coded drive letters do not match user environment.
- No progress feedback for large selections.
- Copying is synchronous inside background worker without cancellation.

## Future Enhancements (Optional)
- Configurable default drives and folder templates.
- Progress indicator and cancel.
- Auto‑import from card reader.
- Additional file types and RAW formats.
- Duplicate handling and logging.
