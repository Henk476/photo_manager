# PRD vs Code Review Findings

- PRD says output root `G:\Work` with `{YYYY}-{MM}-{DD} - {Project}` template, but code hardcodes `F:\{YYYY}\{MM} - {MMMM}` and `{DD} - {Project}` in `Form1.cs`; no `G:\Work` anywhere.
- PRD requires configurable default input drive, output root, and folder template; code has no configuration mechanism or settings storage.
- PRD says file picker should default to a removable memory card when available; code always sets `InitialDirectory = "G:\\"`.
- PRD requires RAW formats (`.nef .cr2 .cr3 .arw .dng .raf .orf .rw2 .pef .srw`) to be copied to `RAW`; code only handles `.NEF`.
- PRD requires video formats (`.mp4 .mov .avi .mxf`) copied to output root; code ignores all video.
- PRD says image formats should be ignored during copy; code still copies `.JPG` files to the output root.
- PRD lists `.jpeg` as selectable but the copy logic doesn’t handle `.JPEG` (or `.jpg` case-insensitively via `GetExtension`), so behavior is inconsistent with selection and expectations.
- Output structure creation is incomplete: code only creates `RAW/Edit/Select` if the output folder doesn’t exist, so missing subfolders inside an existing output path are never created.
- The “Output” folder browse flow is broken: `btnOutput_Click` sets `outputPath = fbd.SelectedPath` and then immediately overwrites it with `txtOutputPath.Text`, so selecting a folder does nothing unless the text field already matches.
- PRD says output path should remain consistent when the text field is manually edited; code never validates or reconciles manual edits, and can overwrite with generated paths on `txtProject_Leave`.
- PRD goal mentions `LRC` folder in the Goals section, but functional requirements and code use `Select`; the spec is inconsistent and the code implements only one interpretation.
- UX requirement says minimal errors; code uses `MessageBox` on exceptions but doesn’t show which file failed or how many were skipped, making troubleshooting harder for real-world use.
