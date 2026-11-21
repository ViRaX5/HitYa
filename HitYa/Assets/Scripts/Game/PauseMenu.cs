using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] ButtonClickHandler ButtonClick;

    [SerializeField] GameObject pauseMenu;

    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void ResumeButton()
    {
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
