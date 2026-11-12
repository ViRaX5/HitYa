using UnityEngine;
using TMPro;

public class ButtonClickHandler : MonoBehaviour
{
    public TextMeshProUGUI distance;
    public TextMeshProUGUI buttonText;
    public GameObject button;
    public GameObject angleGauge;
    public GameObject powerBar;

    public PauseButton PauseButton;

    public bool playing = false;

    private int dis = 0;

    void Start()
    {
        distance.text = "";
        powerBar.SetActive(false);
    }

    void Update()
    {
        if(playing && !PauseButton.pause)
        {
            dis++;      //should be delta time thingy
            distance.text = "Distance: " + dis;
        }
    }

    public void OnButtonClick()
    {   
        if(buttonText != null){
            if(buttonText.text == "SET"){
                buttonText.text = "HIT";
                angleGauge.SetActive(false);
                powerBar.SetActive(true);
            }
            else{
                button.SetActive(false);
                powerBar.SetActive(false);
                distance.text = "Distance: " + dis;
                playing = true;
            }
                
        }
        
    }
}
