// SettingsMenu.cs (Assets/Scripts/Game): opens and closes the settings panel in the Home screen
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] GameObject panel;       // the SettingsPanel, hidden at the start

    public void Open() => panel.SetActive(true);     // Gear button: On Click → SettingsMenu.Open

    public void Close() => panel.SetActive(false);   // Close button and the dimmed area: On Click → SettingsMenu.Close
}
