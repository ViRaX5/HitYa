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
  - `Services.Coins`: `ICoinService`, implemented by `CoinService` (balance + distance rewards)

## Architecture

- Scene flow (build order): `Bootstrap` (index 0) → `Home` → `Level`.
  - `Bootstrap`: only `Bootstrap.cs`. Marks itself `DontDestroyOnLoad`, creates the services, then loads `Home`.
  - `Home`: main menu. Shows the coin balance (`CoinCounter`), Play button → `ScreenFlow.Play()` loads `Level`.
  - `Level`: gameplay. `ButtonClickHandler` runs the SET (angle) → HIT (power) → flight/distance loop. `PauseButton` + `PauseMenu` handle pause, resume, retry and home. `FinishRun()` awards coins for the distance.
- Init order: services are created in `Bootstrap.Awake()`. Always enter Play Mode from the `Bootstrap` scene, otherwise `Services` is null.
- Scripts live in `HitYa/Assets/Scripts/` (`Core/`, `Controllers/`, `Game/`, `Services/`, plus some loose scripts at the root).
- Also in the project, currently unused: `Boot` scene with `SceneLoader`, `LoadingSceneController`, `SampleScene`.

## Rules for AI

- Follow the existing folder structure and naming. Don't create any new folders without asking first.
- Don't delete anything (scripts, scenes, assets) without permission, even if it looks dead or unused.
- Don't invent APIs. If unsure about an SDK or Unity API, say so and point to the docs.
- Don't change Player Settings, package name, API levels or build settings unless asked.
- Keep changes small and explain what you changed and why.
- Never commit secrets (keystores, passwords, API keys).
- Screens use Services, never PlayerPrefs
- Decisions are logged in `DECISIONS.md` (newest first), in this format:

  ```
  ## YYYY-MM-DD - Title
  Options: ...
  Chose: ... and why\
  AI: how AI was used, or "Wasn't used"\
  Who: ...
  ```

## Conventions

- [Naming, code style, where scripts go - fill in as a team]
