using System;

public interface ISettingsService
{
    bool SoundOn { get; set; }               // mute toggle, keeps the volume
    int MusicVolume { get; set; }            // 0-100
    event Action MusicVolumeChanged;         // raised when MusicVolume changes
}
