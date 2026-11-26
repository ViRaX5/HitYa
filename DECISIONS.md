## 25-11-2026 - Distance Rewards
Options: 10 coins per 10 meters / 1 coin per 10 meters / 10 coins per 100 meters.\
Chose: 1 coin per 10 meters. With the current run max distance (before perfect bounce) being around 100 meters, and first upgrade (for now) costing 50 gold, this will make almost every run rewarding and progress you towards end goal, doesn't make it over tuned, make sure that bad runs (under 10) are not rewarded at all. Makes first upgrade achieveable in first session (with each round being a few seconds-minute long).\
AI: Wasn't used.\
Who: Team decision, Noam implemented in code

## 24-11-2026 - Stickman Sprite Frame Sizes
Options: Keep the wider flying frames / crop the flying poses to 117 x 252 px / scale down the flying artwork.\
Chose: Use 117 x 252 px frames for afraid, waiting, and walking. Keep the 320 x 320 px flying frames so the poses and motion trails stay intact.\
AI: Measured the pose bounds, updated the upright sprite rectangles, and verified the saved frame sizes.\
Who: Valeri

## 23-11-2026 - Initial Game Loop Values
Option: Realistic values / Semi-Realistic values / Hyper values.\
Chose: Semi-Realistic, hyper will make it hard create a large enough map, and will make it harder to add the flying projectiles in the future. Realistic will make the distance values relatively low, making it less fun/punchy, therefor being less fun. Semi-Realistic is a good middle ground, giving it a good feel, without too low numbers.\
AI: Used to create the distance calculation formula and with the camera movement.\
Who: Amit

## 18-11-2026 - Settings (SoundOn) is Saved Through ISaveService
Options: The Settings service writes to PlayerPrefs by itself / The Sound toggle uses PlayerPrefs directly / The Settings service saves through ISaveService
Chose: Through ISaveService. Only PlayerPrefsSaveService knows we save to PlayerPrefs, so if we ever change how we save (a file, the cloud) we change one class and the Settings and Coins services stay the same. The Sound toggle only talks to Services.Settings and never to PlayerPrefs, like the rule in AGENTS.md. The downside is that ISaveService only saves ints, so SoundOn is saved as 1/0 and is on by default.\
AI: Claude wrote the SettingsService and SoundToggle code and the first draft of this entry. I decided the design, checked it, and tested that the toggle keeps its state.\
Who: Noam

## 18-11-2026 - Coin Service and Distance Rewards
Decision: Add a coin service as the second service. It calculates rewards for distance milestones and uses the save service to keep the coin balance between sessions.\
Rewards: 5 coins at 500 distance, 10 more at 1,000, and 20 more at 1,500; a run reaching 1,500 earns 35 coins in total.\
Why: Rewarding distance gives players an incentive to travel farther, while a separate service keeps coin calculations and storage separate from the game screens.\
AI: Assisted with implementing the service; the distance milestones and cumulative rewards were chosen by the user.\
Who: Valeri

## 12-11-2026 - API Version
Options: minimum version: version 29 / older than 29 / newer than 29
Chose: Version 29 as oldest. Got an old phone from Alex because have an Iphone so needed support of older andriod versions but still within the standard.\
AI: Wasn't used.\
Who: Noam

## 12-11-2026 - Package Name
Options: com.ViRaXGames.HitYa
Chose: Used my in-game name as a studio name because it is under my repo, game name is game name.\
AI: Wasn't used.\
Who: Amit

## 11-11-2026 - Puase overlay buttons
Options: Resume / Restart / Home
Chose: All buttons to give multiple options for the player.\
AI: Wasn't used.\
Who: Amit

## 31-10-2026 - Daily quests that grant rewards.\
Options: Permanent rewards / Limited time boosts.\
Chose: Limited Time Boosts, makes it fair for new players, they are not behind without a chance to catch up.\
AI: Wasn't used.\
Who: Noam for initial thought and the entire team for the refinement

## 31-10-2026 - Additional bounce after landing.\
Options: A bar appears with a quick real time evemt / Visual effects on the character during the perfect hit window / Haptics for the duration of the window.\
Chose: Visual effects and Haptics, don't overload with bars (that we have at the start), intuitive to understand, takes time to master. Mixing haptics with visual give additional stimuli to react to.\ 
AI: Wasn't used.\
Who: Noam and Valeri initial thought, Valeri and Amit refinement

## 31-10-2026 - Keep the loop from being stale.\
Options: Background changes / Music changes
Chose: Combination of both, using both makes it rewarding to travel further, the visual makes it prettier/makes it alive, music brings energy.\
AI: Wasn't used.\
Who: Valeri for background, Noam for Music

## 31-10-2026 - Include progression system for the core loop.\
Options: Prestige that reset and grants points to spend on a skill tree / Unlocking new levels.\
Chose: Unlocking new levels, keeps it simple and rewarding, progress isn't lost, easy to implement.\
AI: Wasn't used.\
Who: Valeri and Amit

## 31-10-2026 - Include a second chance feature.\
Options: Opt in to watch an ad to get the bobus / pay to get the bonus / no second chance.\
Choice: Opt in ad, still gives you the option to get the reward/extra bounce, but isn't abused with money, must spend time to get it.\
AI: Wasn't used.\
Who: Valeri and Amit

## 31-10-2026 - Mid flight engagement.\
Options: Taps to keep him flying / Objects flying towards him.\
Choice: Objects flying towards him, make the engagment an active part, that you need to pay attention to, not mindlessly tapping.\
AI: Wasn't used.\
Who: Amit

## 31-10-2026 - META progression.\
Options: Shop for upgrades to the bat and cosmetics / Buy upgrades for the core loop (like unlockable windtunnels/boosts).\
Choice: Shop for upgrades to the bat and cosmetics, easier to implement at the moment, something basic that will be expected to be in the game, keeps the core loop simple without adding to many mechanics.\
AI: Helped with refining the ideas.\
Who: Everyone

## 31-10-2026 - Free to play vs pay to win.\
Options: Keep certain upgrades behind a paywall / Only unlocks by playing / Buy using in-game currency that you can get by playing or buy/watch ads to speed things up.\
Choice: Buy using in-game currency, makes it fair for everyone, you don't have to pay/watch ads, but it may speed things up.\
AI: Wasn't used.\
Who: Valeri and Amit

## 31-10-2026 - Start of the round mechanic.\
Options: Bar for strength and bar for angle that you need to click at the right time / Eyeball the angle, tap fast in a short window of the for strength.\
Choice: Bar for strength and bar for angle, keeps it clear for the player what is the sweet spot to hit, relying on his timings ability, doesn't leave room for misinterpitation.\
AI: Wasn't used.\
Who: Everyone

## 31-10-2026 - Rewards system.\
Options: Rewards for daily login / Rewards for returning players who were away for a while.\
Choice: Both, make it enticing to play daily (the rewards worth more), but also help a returning playing get back into the action
AI: Wasn't used.\
Who: Everyone

## 31-10-2026 - Customization of the character.\
Options: Paid skins from the start / Unlockables as rewards.\
Choice: Unlockables as rewads, give the option to customize as a reward for reaching a certain level to give a reason to play more and gives us something to treat them with (new skins).
AI: Wasn't used.\
Who: Everyone

## 31-10-2026 - Game Looks.\
Options: 3d character and world / 2d highly articulated character and world / stickman world.\
Choice: stickman world, easier to create, easy to make a consistent theme, gives room for creativity in the basic world.\
AI: Wasn't used.\
Who: Everyone
