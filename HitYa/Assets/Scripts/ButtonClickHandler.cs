using UnityEngine;
using TMPro;

public class ButtonClickHandler : MonoBehaviour
{
    public TextMeshProUGUI buttonText;
    public GameObject container;
    public GameObject angleGauge;
    public GameObject powerBar;
    public void OnButtonClick()
    {   
        if(buttonText != null){
            if(buttonText.text == "SET"){
                buttonText.text = "HIT";
                angleGauge.SetActive(false);
            }
            else{
                container.SetActive(false);
                powerBar.SetActive(false);
            }
                
        }
        
    }
}
