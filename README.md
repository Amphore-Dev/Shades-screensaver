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

**Requirements:** macOS 11.5+, Xcode with macOS SDK

### Project Structure

```
MacOS/
├── Shades.xcodeproj
└── Shades/
    ├── ShadesView.swift            # ScreenSaverView engine
    ├── types/                      # Data types (ShadeColor, ShadePoint, ShadeConfig…)
    ├── constants/                  # Colors, filters, constants
    ├── utils/                      # Math, color and config utilities
    ├── shapes/                     # BaseShape + 5 shape implementations
    └── Resources/fonts/
        └── Remained.ttf            # Custom font
```

### Build from source

```bash
cd MacOS

# Build release
make

# Build + install to ~/Library/Screen Savers/
make install

# Remove
make remove

# Clean build artifacts
make clean
```

After `make install`, open **System Settings → Screen Saver** and select **Shades**.

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
