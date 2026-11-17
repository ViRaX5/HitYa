// SoundToggle.cs (Assets/Scripts/Game): on your Sound toggle in the Home screen
using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{
    [SerializeField] Toggle toggle;          // drag your Sound toggle here

    void Start() => toggle.isOn = Services.Settings.SoundOn;

    public void SetSound(bool isOn)          // Toggle: On Value Changed (Boolean) → SoundToggle.SetSound (Dynamic bool)
    {
        Services.Settings.SoundOn = isOn;
    }
}
