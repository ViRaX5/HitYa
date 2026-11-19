using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject container;
    //public PauseButton PauseButton;

    public ButtonClickHandler ButtonClick;

    [SerializeField] GameObject pauseMenu;

    //void Awake()
    //{
    //    container.SetActive(false);
    //}
    //void Update()
    //{
    //    if (PauseButton.pause)
    //    {
    //        container.SetActive(true);
    //        Time.timeScale = 0;
    //    }
    //}

    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void ResumeButton()
    {
        //PauseButton.pause = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void HomeButton()
    {
        ButtonClick.FinishRun();
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Home");
    }

    public void RetryButton()
    {
        ButtonClick.FinishRun();
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level");
    }
}
