using UnityEngine;
using TMPro;

public class ButtonClickHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI distance;
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] GameObject button;
    [SerializeField] GameObject angleGauge;
    [SerializeField] GameObject powerBar;

    [SerializeField] GameObject angleArrow;
    [SerializeField] GameObject powerArrow;

    [SerializeField] Launcher stickmanLauncher;

    [SerializeField] double strength = 100;

    private double angleArrowMax = 745;
    private double angleArrowMin = 178;

    private double angleHit = 0;

    private Vector3 angleArrowPosition;

    private double powerArrowMax = 785.9464; //checked in game this the the max value
    private double powerArrowMin = 159.9796; //checked in game this the the min value

    private double powerHit = 0;

    private Vector3 powerArrowPosition;


    public bool playing = false;

    private int dis = 0;
    private bool runStarted = false;
    private bool rewardClaimed = false;

    void OnEnable()
    {
        if (stickmanLauncher != null)
            stickmanLauncher.OnLanded += HandleLanded;
    }

    void OnDisable()
    {
        if (stickmanLauncher != null)
            stickmanLauncher.OnLanded -= HandleLanded;
    }

    void Update()
    {
        if (playing && stickmanLauncher != null)
        {
            dis = stickmanLauncher.CurrentDistance;
            distance.text = "Distance: " + dis;
        }
    }

    private void HandleLanded(int finalDistance)
    {
        dis = finalDistance;
        distance.text = "Distance: " + dis;
        FinishRun(); // Calls GameSystems.Coins.AwardForDistance(dis)
    }

    void Start()
    {
        // distance.text = "";
        powerBar.SetActive(false);
        powerArrow.SetActive(false);
    }

    //void Update()
    //{
    //    if (playing)
    //    {
    //        dis++;      //should be delta time thingy
    //        distance.text = "Distance: " + dis;
    //    }
        //if(powerArrow.transform.localPosition.y > powerArrowMax)
        //{
        //    powerArrowMax = powerArrow.transform.localPosition.y;
        //}
        //if (powerArrow.transform.localPosition.y < powerArrowMin)
        //{
        //    powerArrowMin = powerArrow.transform.localPosition.y;
        //}
        //if (angleArrow.transform.localPosition.y > angleArrowMax)
        //{
        //    angleArrowMax = angleArrow.transform.localPosition.y;
        //}
        //if (angleArrow.transform.localPosition.y < angleArrowMin)
        //{
        //    angleArrowMin = powerArrow.transform.localPosition.y;
        //}
    //}

    public void OnButtonClick()
    {
        if (buttonText != null)
        {
            if (buttonText.text == "SET")
            {
                angleArrowPosition = angleArrow.transform.localPosition;
                double angleClamped = System.Math.Clamp((angleArrowPosition.y - angleArrowMin) / (angleArrowMax - angleArrowMin), 0.0, 1.0);
                angleHit = (1.0 - angleClamped) * 90.0;

                buttonText.text = "HIT";
                angleGauge.SetActive(false);
                angleArrow.SetActive(false);
                powerBar.SetActive(true);
                powerArrow.SetActive(true);
            }
            else
            {
                powerArrowPosition = powerArrow.transform.localPosition;
                double powerClamped = System.Math.Clamp((powerArrowPosition.y - powerArrowMin) / (powerArrowMax - powerArrowMin), 0.0, 1.0);
                powerHit = powerClamped * strength;

                stickmanLauncher.Launch(angleHit, powerHit);

                button.SetActive(false);
                powerBar.SetActive(false);
                powerArrow.SetActive(false);
                distance.text = "Distance: " + dis;
                runStarted = true;
                playing = true;
                Debug.Log("Power Max: " + powerArrowMax + " Power Min: " + powerArrowMin);
                Debug.Log("Angle Max: " + angleArrowMax + " Angle Min: " + angleArrowMin);
                Debug.Log("Angle Hit: " + angleHit);
                Debug.Log("Power Hit: " + powerHit);
            }

        }

    }

    public void PausedClicked()
    {
        playing = false;
    }

    public void ResumeClicked()
    {
        playing = runStarted && !rewardClaimed;
    }

    // Temporary end-of-run hook until landing and game-over logic are implemented.
    public void FinishRun()
    {
        if (!runStarted || rewardClaimed) return;
        if (GameSystems.Coins == null)
        {
            Debug.LogError("Coin service is unavailable. Start the game from Bootstrap.");
            return;
        }

        int reward = GameSystems.Coins.AwardForDistance(dis);
        rewardClaimed = true;
        playing = false;
        Debug.Log($"Run finished at distance {dis}: earned {reward} coins.");
    }
}
