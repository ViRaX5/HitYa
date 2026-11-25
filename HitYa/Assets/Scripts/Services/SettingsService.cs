using System;
using UnityEngine;

public class SettingsService : ISettingsService
{
    private const string SoundOnKey = "soundOn";
    private const string MusicVolumeKey = "musicVolume";
    private readonly ISaveService save;

    public event Action MusicVolumeChanged;

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

    // 0-100, saved as an int. Full volume by default.
    public int MusicVolume
    {
        get => save.GetInt(MusicVolumeKey, 100);
        set
        {
            save.SetInt(MusicVolumeKey, Mathf.Clamp(value, 0, 100));
            MusicVolumeChanged?.Invoke();
        }
    }
}
