using UnityEngine;
using TMPro;

public class ButtonClickHandler : MonoBehaviour
{
    public GameObject distance;
    public TextMeshProUGUI buttonText;
    public GameObject button;
    public GameObject angleGauge;
    public GameObject powerBar;

    void Start()
    {
        distance.SetActive(false);
        powerBar.SetActive(false);
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
                distance.SetActive(true);
            }
                
        }
        
    }
}
