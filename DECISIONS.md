## 18-11-2026 - Settings (SoundOn) is saved through ISaveService
Options: The Settings service writes to PlayerPrefs itself / The Sound toggle on Home uses PlayerPrefs directly / The Settings service saves through ISaveService
Chose: Through ISaveService. The Settings service only says "save this int under this key", and only PlayerPrefsSaveService knows it is PlayerPrefs. If we move to another kind of save later (a file, the cloud) we change one class and the Settings and Coins services stay the same. Screens like the Sound toggle only talk to Services.Settings, never to PlayerPrefs, which is also the rule in AGENTS.md. The price is that ISaveService only stores ints, so SoundOn is saved as 1/0 and defaults to on when nothing is saved yet\
AI: Claude wrote the code for ISettingsService, SettingsService and the Sound toggle, explained it line by line, and drafted the wording of this entry. I chose the design, reviewed the code and tested that the toggle keeps its state. Claude's note: storing a bool as 1/0 is a small workaround, if more bool settings come we could add GetBool/SetBool to ISaveService\
Who: Noam

## 18-11-2026 - Coin service and distance rewards
Decision: Add a coin service as the second service. It calculates rewards for distance milestones and uses the save service to keep the coin balance between sessions.\
Rewards: 5 coins at 500 distance, 10 more at 1,000, and 20 more at 1,500; a run reaching 1,500 earns 35 coins in total.\
Why: Rewarding distance gives players an incentive to travel farther, while a separate service keeps coin calculations and storage separate from the game screens.\
AI: Assisted with implementing the service; the distance milestones and cumulative rewards were chosen by the user.
Who: valeri

## 12-11-2026 - API Version
Options: minimum version: version 29 / older than 29 / newer than 29
Chose: Version 29 as oldest. Got an old phone from Alex because have an Iphone so needed support of older andriod versions but still within the standard\
AI: Wasn't used\
Who: Noam

## 12-11-2026 - Package Name
Options: com.ViRaXGames.HitYa
Chose: Used my in-game name as a studio name because it is under my repo, game name is game name\
AI: Wasn't used\
Who: Amit

## 11-11-2026 - Puase overlay buttons
Options: Resume / Restart / Home
Chose: All buttons to give multiple options for the player\
AI: Wasn't used\
Who: Amit

## 31-10-2026 - Daily quests that grant rewards\
Options: Permanent rewards / Limited time boosts\
Chose: Limited Time Boosts, makes it fair for new players, they are not behind without a chance to catch up\
AI: Wasn't used\
Who: Noam for initial thought and the entire team for the refinement

## 31-10-2026 - Additional bounce after landing\
Options: A bar appears with a quick real time evemt / Visual effects on the character during the perfect hit window / Haptics for the duration of the window\
Chose: Visual effects and Haptics, don't overload with bars (that we have at the start), intuitive to understand, takes time to master. Mixing haptics with visual give additional stimuli to react to\ 
AI: Wasn't used\
Who: Noam and Valeri initial thought, Valeri and Amit refinement

## 31-10-2026 - Keep the loop from being stale\
Options: Background changes / Music changes
Chose: Combination of both, using both makes it rewarding to travel further, the visual makes it prettier/makes it alive, music brings energy\
AI: Wasn't used\
Who: Valeri for background, Noam for Music

## 31-10-2026 - Include progression system for the core loop\
Options: Prestige that reset and grants points to spend on a skill tree / Unlocking new levels\
Chose: Unlocking new levels, keeps it simple and rewarding, progress isn't lost, easy to implement\\
AI: Wasn't used\
Who: Valeri and Amit

## 31-10-2026 - Include a second chance feature\
Options: Opt in to watch an ad to get the bobus / pay to get the bonus / no second chance\
Choice: Opt in ad, still gives you the option to get the reward/extra bounce, but isn't abused with money, must spend time to get it\
AI: Wasn't used\
Who: Valeri and Amit

## 31-10-2026 - Mid flight engagement\
Options: Taps to keep him flying / Objects flying towards him\
Choice: Objects flying towards him, make the engagment an active part, that you need to pay attention to, not mindlessly tapping\
AI: Wasn't used\
Who: Amit

## 31-10-2026 - META progression\
Options: Shop for upgrades to the bat and cosmetics / Buy upgrades for the core loop (like unlockable windtunnels/boosts)\
Choice: Shop for upgrades to the bat and cosmetics, easier to implement at the moment, something basic that will be expected to be in the game, keeps the core loop simple without adding to many mechanics\
AI: Helped with refining the ideas\
Who: Everyone

## 31-10-2026 - Free to play vs pay to win\
Options: Keep certain upgrades behind a paywall / Only unlocks by playing / Buy using in-game currency that you can get by playing or buy/watch ads to speed things up\
Choice: Buy using in-game currency, makes it fair for everyone, you don't have to pay/watch ads, but it may speed things up\
AI: Wasn't used\
Who: Valeri and Amit

## 31-10-2026 - Start of the round mechanic\
Options: Bar for strength and bar for angle that you need to click at the right time / Eyeball the angle, tap fast in a short window of the for strength\
Choice: Bar for strength and bar for angle, keeps it clear for the player what is the sweet spot to hit, relying on his timings ability, doesn't leave room for misinterpitation\
AI: Wasn't used\
Who: Everyone

## 31-10-2026 - Rewards system\
Options: Rewards for daily login / Rewards for returning players who were away for a while\
Choice: Both, make it enticing to play daily (the rewards worth more), but also help a returning playing get back into the action
AI: Wasn't used\
Who: Everyone

## 31-10-2026 - Customization of the character\
Options: Paid skins from the start / Unlockables as rewards\
Choice: Unlockables as rewads, give the option to customize as a reward for reaching a certain level to give a reason to play more and gives us something to treat them with (new skins).
AI: Wasn't used\
Who: Everyone

## 31-10-2026 - Game Looks\
Options: 3d character and world / 2d highly articulated character and world / stickman world\
Choice: stickman world, easier to create, easy to make a consistent theme, gives room for creativity in the basic world\
AI: Wasn't used\
Who: Everyone
