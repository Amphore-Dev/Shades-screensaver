// swift-tools-version: 6.0
import PackageDescription

let package = Package(
    name: "amphore-saver",
    platforms: [.macOS("11.5")],
    products: [
        .library(name: "ScreensaverCore", targets: ["ScreensaverCore"]),
        .library(name: "ShadesKit", targets: ["ShadesKit"]),
    ],
    targets: [
        .target(name: "ScreensaverCore"),
        .target(name: "ShadesKit", dependencies: ["ScreensaverCore"], resources: [.copy("Resources/Remained.ttf")]),
    ]
)
