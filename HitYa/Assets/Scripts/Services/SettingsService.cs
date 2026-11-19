using System;
using UnityEngine;

public class SettingsService : ISettingsService
{
    private const string SoundOnKey = "soundOn";
    private readonly ISaveService save;

    public SettingsService(ISaveService saveService)
    {
        save = saveService ?? throw new ArgumentNullException(nameof(saveService));
    }

    // Saved as 1 (on) / 0 (off) because ISaveService only saves ints. Sound is on by default.
    public bool SoundOn
    {
        get => save.GetInt(SoundOnKey, 1) == 1;
        set
        {
            save.SetInt(SoundOnKey, value ? 1 : 0);
            AudioListener.pause = !value;    // sound off = pause all audio
        }
    }
}
