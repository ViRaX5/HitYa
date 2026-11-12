using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject container;
    public PauseButton PauseButton;

    void Awake()
    {
        container.SetActive(false);
    }
    void Update()
    {
        if (PauseButton.pause) //escape is for testing
        {
            container.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ResumeButton()
    {
        PauseButton.pause = false;
        container.SetActive(false);
        Time.timeScale = 1;
    }

    public void HomeButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scene_Home"); //need to add valeri's scene here
    }

    public void RetryButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level");
    }
}
