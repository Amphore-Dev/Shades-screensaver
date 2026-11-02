![Shades preview](preview.gif)

# Shades — Screensaver

A screensaver that generates animated geometric compositions with shifting shades of color. Originally ported from the [Shades web project](https://github.com/Amphore-Dev/Shades) (TypeScript/Canvas) to native Swift, then ported again to Windows (C# / WinForms).

## ⚠️ Epilepsy Warning

The shades can produce rapidly flashing and shifting patterns that may trigger seizures in photosensitive individuals. Viewer discretion is advised.

## Shapes

The screensaver randomly cycles through five shape types:

- **Square** — filled or stroked rectangles
- **Circle** — filled or stroked ellipses
- **Spiral** — rotating arcs
- **Triangle** — filled or stroked triangles with optional rotation
- **Text** — displays the current time (HH:mm) using the custom _Remained_ font

Each scene is composed of a grid of shapes drawn multiple times with decreasing opacity, creating the signature "shades" effect. The offset drifts smoothly to animate the composition, and scenes auto-regenerate with a fade transition.

---

## macOS

**Install:** download the signed and notarized **Shades.saver.zip** from the [latest release](https://github.com/Amphore-Dev/Shades-macos-screensaver/releases/latest), unzip it and double-click **Shades.saver**, then select it in **System Settings → Screen Saver**. macOS 11.5+, Apple silicon and Intel. Click **Options…** to tune it.

Want Kinogrida, Shades and Psykala mixed together? Get **amphore-saver** from [amphore-saver](https://github.com/Amphore-Dev/amphore-saver).

**Source:** the `MacOS/` folder is generated from [amphore-saver](https://github.com/Amphore-Dev/amphore-saver), where the macOS code is maintained (see `MacOS/README.md`). Make changes there and re-export with `scripts/export-saver.sh Shades <path>/MacOS`.

```bash
cd MacOS
make install   # requires Xcode 16+ and XcodeGen (brew install xcodegen)
```

---

## Windows

**Requirements:** Windows 10+, [.NET 9 Runtime](https://dotnet.microsoft.com/download/dotnet/9.0)

### Project Structure

```
Windows/
├── Shades.csproj
├── Program.cs
├── ScreensaverForm.cs
├── PreviewForm.cs
├── build.ps1
├── Makefile
├── types/
├── constants/
├── utils/
├── shapes/
└── fonts/
    └── Remained.ttf
```

### Build from source

```powershell
cd Windows

# Build
.\build.ps1

# Run fullscreen
.\build.ps1 run

# Publish single-file exe
.\build.ps1 publish

# Install to C:\Windows\System32\Shades.scr
.\build.ps1 install

# Remove
.\build.ps1 remove
```

Or using Make:

```bash
make           # build
make run       # build + run
make install   # build + install
make remove    # uninstall
make clean     # clean artifacts
```

### Manual install

1. Build with `.\build.ps1 publish`
2. Copy `bin\publish\Shades.exe` to `C:\Windows\System32\Shades.scr`
3. Right-click the desktop → **Personalize → Lock screen → Screen saver**
4. Select **Shades** from the list
