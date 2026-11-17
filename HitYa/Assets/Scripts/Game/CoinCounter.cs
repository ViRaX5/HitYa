// CoinCounter.cs (Assets/Scripts/Game): on your coin text in the HUD
using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] TMP_Text label;         // drag your coin text here

    void Start() => Show();

    public void Add(int amount)              // a button: On Click () → CoinCounter.Add, 10
    {
        Services.Coins.AddCoins(amount);
        Show();
    }

    void Show() => label.text = Services.Coins.Balance.ToString();
}
