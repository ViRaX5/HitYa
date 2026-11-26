using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic backgroundMusic;
    [SerializeField] AudioSource music;      // the music's AudioSource, on this object

    void Awake() {
        if(backgroundMusic == null)
        {
            backgroundMusic = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Follows the music volume setting (0-100) while the game runs
    void OnEnable()
    {
        if (backgroundMusic != this) return;
        Services.Settings.MusicVolumeChanged += ApplyVolume;
        ApplyVolume();
    }

    void OnDisable()
    {
        if (backgroundMusic == this) Services.Settings.MusicVolumeChanged -= ApplyVolume;
    }

    void ApplyVolume() => music.volume = Services.Settings.MusicVolume / 100f;
}
