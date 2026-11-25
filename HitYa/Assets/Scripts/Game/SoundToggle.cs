// SoundToggle.cs (Assets/Scripts/Game): on your Sound toggle in the Home screen
using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{
    [SerializeField] Toggle toggle;          // drag your Sound toggle here
    [SerializeField] MusicVolumeSlider volumeSlider;    // the music slider, shows 0 while muted
    [SerializeField] GameObject waves;       // the sound waves icon, hidden when muted or at volume 0

    void Start() => Refresh();

    public void SetSound(bool isOn)          // Toggle: On Value Changed (Boolean) → SoundToggle.SetSound (Dynamic bool)
    {
        Services.Settings.SoundOn = isOn;
        volumeSlider.Refresh();
        Refresh();
    }

    // Show the saved mute state without triggering the change event
    public void Refresh()
    {
        toggle.SetIsOnWithoutNotify(Services.Settings.SoundOn);
        waves.SetActive(Services.Settings.SoundOn && Services.Settings.MusicVolume > 0);
    }
}
