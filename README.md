


<img width="1254" height="1254" alt="7bf66f2c-a3a5-4b4b-8c78-0f019bf0d339" src="https://github.com/user-attachments/assets/169ccf33-e416-488f-9d7a-6ea41b8f8329" />


# OpenCode AI — Portable USB Creator

Turn any USB stick into a **self-contained AI coding assistant** that runs on any
Windows 10/11 PC. No installation. No admin rights on the target machine. Nothing
left behind on the computer you plug it into.

`OpenCode-USB-Creator.exe` partitions and formats a USB stick, then unpacks a fully
portable copy of [OpenCode](https://opencode.ai) — a terminal AI coding agent —
together with its own bundled terminal (WezTerm) and JavaScript runtime (Node.js).
Everything runs from the stick.

---

## Download & use

1. Download **`OpenCode-USB-Creator.exe`** from the
   [Releases](../../releases) page.
2. Right-click it → **Run as administrator** (it repartitions a drive, so it needs this).
3. Pick your USB stick, choose a partition style (**MBR** is the most compatible),
   and click through.
4. When it finishes, eject the stick.
5. Plug it into any Windows 10/11 PC, open it in File Explorer, and double-click
   **`OpenCode AI.exe`**.

> **First run:** Windows SmartScreen may warn about an unsigned app
> ("Windows protected your PC"). Click **More info → Run anyway**. This is expected
> for any app that isn't code-signed — it isn't a sign of malware, but don't take
> that on faith: the full source is in [`src/`](src/) so you can build it yourself.

---

## What you get on the stick

| Component | Purpose |
|-----------|---------|
| **OpenCode** v1.18.3 | Terminal AI coding agent |
| **WezTerm** | GPU-accelerated portable terminal |
| **Node.js** v26.5.0 | Bundled JavaScript runtime |
| **ripgrep** | Fast search (bundled, no download needed) |

It works out of the box with OpenCode's free built-in model — **no API key required**
to start. To use your own provider (OpenAI, Anthropic/Claude, Google Gemini, GitHub
Copilot, a local Ollama, and 70+ others), run `/connect` inside OpenCode or add a key
to `data\config\opencode.json`.

---

## Stays on the stick, not the host

This is the whole point of the project. Credentials, session history, cache, logs and
temp files are all redirected onto the USB drive — the app writes **nothing** into the
host user's profile. Verified by snapshotting the host's temp/AppData folders, running
the stick, and diffing: zero new application files.

**One honest limit:** Windows *itself* records that a USB device was connected (in the
registry and Event Log), no matter what any app does. That's outside this project's
control. What's guaranteed is that *your* files, keys and conversations never touch the
host's disk — not that the machine has no idea a stick was plugged in.

---

## Works on any drive letter

USB sticks get whatever drive letter Windows hands out, and it changes from PC to PC.
Everything on the stick locates itself at runtime, so it works whether it mounts as
`D:`, `E:`, or anything else — including a fully self-contained `PATH` so it uses the
stick's own Node.js and OpenCode rather than anything installed on the host.

---

## Building it yourself

The [`src/`](src/) folder contains the full source:

- `OpenCode-USB-Creator.cs` — the installer (C#/WinForms)
- `launcher.cs` — the tiny self-locating launcher that becomes `OpenCode AI.exe`
- `build-embedded.ps1` — zips the payload and compiles the installer
- `create-usb.ps1` — a standalone PowerShell version of the installer logic
- icons and splash image

Building also needs the **payload** — the actual OpenCode, Node.js and WezTerm binaries
that get embedded into the `.exe`. Those are large third-party downloads and are **not**
included in this repository (see [Licenses](#licenses)). Grab them from:

- OpenCode: <https://opencode.ai>
- Node.js (Windows x64): <https://nodejs.org>
- WezTerm: <https://wezterm.org>
- ripgrep: <https://github.com/BurntSushi/ripgrep/releases>

Arrange them into a folder matching the layout the installer expects
(`bin\`, `nodejs\`, `wezterm\`, `config\`, `data\`), then run:

```powershell
.\build-embedded.ps1 -SourcePath "<payload folder>"
```

---

## Requirements

- Windows 10/11, 64-bit
- A USB stick (all data on it will be erased)
- Administrator rights **on the machine that creates the stick** (not on machines that run it)
- Internet only if you use a cloud AI provider

---

## Licenses

This project's own code (the installer and launcher) is released under the
[MIT License](LICENSE).

It bundles and redistributes third-party software, each under its own license —
OpenCode, Node.js, WezTerm and ripgrep. Those binaries are **not** in this repository;
they're downloaded from their official sources at build time and embedded into the
released `.exe`. See [LICENSE](LICENSE) for attribution and links.
