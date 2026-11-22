using TMPro;
using UnityEngine;

public class RoundOver : MonoBehaviour
{
    [SerializeField] ButtonClickHandler ButtonClick;

    [SerializeField] GameObject roundOver;

    [SerializeField] Launcher stickmanLauncher;

    [SerializeField] TextMeshProUGUI coinsText;
    [SerializeField] TextMeshProUGUI distanceText;

    private void OnEnable()
    {
        if(stickmanLauncher != null)
        {
            stickmanLauncher.OnLanded += HandleLanded;
        }
    }

    private void OnDisable()
    {
        if(stickmanLauncher != null)
        {
            stickmanLauncher.OnLanded -= HandleLanded;
        }
    }

    private void HandleLanded(int finalDistance)
    {
        int coinsEarned = ButtonClick != null ? ButtonClick.FinishRun() : 0;

        if (coinsText != null)
            coinsText.text = $"You've earned {coinsEarned} coins!";

        if (distanceText != null)
            distanceText.text = $"You reached distance of: {finalDistance}m";

        EndRound();
    }

    public void EndRound()
    {
        Time.timeScale = 0;
        roundOver.SetActive(true);
        
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
