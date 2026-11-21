using UnityEngine;
using UnityEngine.SceneManagement;

// The only script in the Bootstrap scene, the first scene in the Scene List.
public class Bootstrap : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);       // Services survives every scene change

        // Start every service here, in order (next follow-along)
        Services.Save = new PlayerPrefsSaveService();
        Services.Settings = new SettingsService(Services.Save);
        GameSystems.Coins = new CoinService(Services.Save);

        AudioListener.pause = !Services.Settings.SoundOn;    // apply the saved sound setting on start

        Debug.Log("Bootstrap: services ready");
        SceneManager.LoadScene("Home");      // only now: the first screen
    }
}
