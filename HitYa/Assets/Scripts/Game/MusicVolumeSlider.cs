// MusicVolumeSlider.cs (Assets/Scripts/Game): on the music volume Slider in the settings panel
using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeSlider : MonoBehaviour
{
    [SerializeField] Slider slider;          // drag the Slider here (0-100, whole numbers)
    [SerializeField] SoundToggle soundToggle;    // the mute toggle next to it

    void Start() => Refresh();

    // Show 0 while muted, the saved volume otherwise, without triggering the change event
    public void Refresh()
    {
        slider.SetValueWithoutNotify(Services.Settings.SoundOn ? Services.Settings.MusicVolume : 0);
    }

    public void SetVolume(float value)       // Slider: On Value Changed (Single) → MusicVolumeSlider.SetVolume (Dynamic float)
    {
        if (!Services.Settings.SoundOn)      // dragging the slider while muted turns the sound back on
        {
            Services.Settings.SoundOn = true;
            soundToggle.Refresh();
        }
        Services.Settings.MusicVolume = Mathf.RoundToInt(value);
        soundToggle.Refresh();               // volume 0 shows the muted icon
    }
}
