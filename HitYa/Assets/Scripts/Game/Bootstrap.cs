using UnityEngine;
using UnityEngine.SceneManagement;

// The only script in the Bootstrap scene, the first scene in the Scene List.
public class Bootstrap : MonoBehaviour
{
    [SerializeField] EconomyConfig economy;        // drag the asset here
    void Awake()
    {
        DontDestroyOnLoad(gameObject);       // Services survives every scene change

        // Start every service here, in order (next follow-along)
        Services.Save = new PlayerPrefsSaveService();
        Services.Settings = new SettingsService(Services.Save);
        GameSystems.Coins = new CoinService(Services.Save);

        AudioListener.pause = !Services.Settings.SoundOn;    // apply the saved sound setting on start

        // Bootstrap.cs: a field, and one line right after Save
        Services.Config = new ScriptableObjectConfigService(economy);   // 2 · config

        Debug.Log("Bootstrap: services ready");
        SceneManager.LoadScene("Home");      // only now: the first screen
    }
}
