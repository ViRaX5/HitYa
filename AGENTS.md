## The game

You hit a stick-man with a bat as strong as you can so it will travel the farthest.
You need to hit it hard, but also the placement of the hit matters.
The further it flies, the higher score you get.
During each round you will earn points/gold you can spend on upgrades to the bat.
As you progress, you level up, giving you points you can use to upgrade the stick-man stats/bounciness.
There will be daily quests that grant you a limited time power-up/multiplier for the score, based on the quest's difficulty.

## Tech

- Engine: Unity 6.6 (6000.6.7f1), C#
- Target: Android (portrait), minimum API level 29
- Repo: https://github.com/ViRaX5/HitYa.git (Unity project lives in `HitYa/`)
- Packages:
  - Universal Render Pipeline (2D renderer)
  - Input System
  - uGUI + TextMesh Pro
  - 2D Animation, Aseprite Importer, PSD Importer, Sprite Shape, Tilemap
  - Test Framework
  - Unity MCP (`com.coplaydev.unity-mcp`) for AI editor access
- Services (static `Services` class):
  - `Services.Save`: `ISaveService`, implemented by `PlayerPrefsSaveService`
  - `Services.Settings`: `ISettingsService`, implemented by `SettingsService` (`SoundOn`, on by default, saved through `ISaveService`)
  - `Services.Coins`: `ICoinService`, implemented by `CoinService` (balance + distance rewards)

## Architecture

- Scene flow (build order): `Bootstrap` (index 0) → `Home` → `Level`.
  - `Bootstrap`: only `Bootstrap.cs`. Marks itself `DontDestroyOnLoad`, creates the services in order (Save, Settings, Coins), then loads `Home`.
  - `Home`: main menu. Shows the coin balance (`CoinCounter`) and a gem display (UI only, no script or service behind it yet). `SoundToggle` on the Sound toggle reads and writes `Services.Settings.SoundOn`. Play button → `ScreenFlow.Play()` loads `Level`. `BackgroundMusic` keeps the music object across scenes, and changing `Services.Settings.SoundOn` can pause or resume the `AudioListener`.
  - `Level`: gameplay. `ButtonClickHandler` runs the SET (angle) → HIT (power) flow and a placeholder distance counter that increases each frame; real character flight and landing are not implemented yet. The pause button calls `PauseMenu.Pause()` (freezes time, shows the menu) and `ButtonClickHandler.PausedClicked()`. Resume calls `PauseMenu.ResumeButton()` and `ButtonClickHandler.ResumeClicked()`. Home and Retry call `FinishRun()` (awards coins for the distance milestones), then load `Home` or `Level`.
- Assemblies:
  - `Services` (`Scripts/Services/Services.asmdef`) references nothing.
  - `Game` (`Scripts/Game/Game.asmdef`) covers `Scripts/Game/` and its subfolders and references `Services`, TextMesh Pro, UnityEngine.UI and Input System.
  - `Services` must never reference `Game`.
- Init order: services are created in `Bootstrap.Awake()`. Always enter Play Mode from the `Bootstrap` scene, otherwise `Services` is null. A Multiplayer Play Mode configuration (`Assets/Settings/PlayMode/Bootstrap.asset`) starts Play Mode from `Bootstrap`, but which configuration is active is saved per machine in `UserSettings` (not committed), so check it is selected.
- Scripts live in `HitYa/Assets/Scripts/Game/` (including `Core/` and `Controllers/`) and `HitYa/Assets/Scripts/Services/`.
- Also in the project, currently unused: `Boot` scene with `SceneLoader`, `LoadingSceneController`, `SampleScene`, `PauseButton.cs` (its `pressedPause()` is no longer wired to any button).

## Rules for AI

- Follow the existing folder structure and naming. Don't create any new folders without asking first.
- Don't delete anything (scripts, scenes, assets) without permission, even if it looks dead or unused.
- Don't invent APIs. If unsure about an SDK or Unity API, say so and point to the docs.
- Don't change Player Settings, package name, API levels or build settings unless asked.
- Keep changes small and explain what you changed and why.
- Write code only the way it is already written in the project's scripts. If something new is needed (an API, syntax or pattern not used yet), explain what it is and why before adding it.
- Never commit secrets (keystores, passwords, API keys).
- Screens use Services, never PlayerPrefs
- Decisions are logged in `DECISIONS.md` (newest first), in this format (dates are day first):

  ```
  ## DD-MM-YYYY - Title
  Options: ...
  Chose: ... and why\
  AI: how AI was used, or "Wasn't used"\
  Who: ...
  ```

## Conventions

- Where scripts go: scripts that belong to a screen or the scene flow (MonoBehaviours such as `SoundToggle`, `ScreenFlow`, `Bootstrap`) go in `Scripts/Game/`. Services and their interfaces go in `Scripts/Services/`. Code in `Services/` must never use anything from `Game/`.
- Naming: classes, interfaces, methods and properties are `PascalCase`. Interfaces start with `I` (`ICoinService`). A service implementation is named after what it does (`CoinService`, `SettingsService`, `PlayerPrefsSaveService`). One class per file, and the file is named like the class.
- Fields: private fields and locals are `camelCase`, constants are `PascalCase` (`CoinsKey`). References set in the Inspector are written `[SerializeField] Type name;` and are not `public`.
- Code style: 4-space indent, opening brace on its own line, `using` lines at the top of the file. Don't reformat existing lines just to change the style.
- Services: screens only use the `Services` class (`Services.Coins`, `Services.Settings`, `Services.Save`). A new service gets an interface, an implementation, a property in `Services.cs`, and is created in `Bootstrap.Awake()`. A service that needs to save gets `ISaveService` in its constructor.
- Saving: only `PlayerPrefsSaveService` touches `PlayerPrefs`. `ISaveService` only saves ints (a bool is 1/0).
- Scenes: scenes are loaded by name (`"Home"`, `"Level"`) and must be in the Scene List.
- Comments: short comments that say what a script is for and which button or event it connects to, as in the existing scripts.
