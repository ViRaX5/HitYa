using UnityEngine;

// EconomyConfig.cs (Assets/Scripts/Services): every economy number in one asset
[CreateAssetMenu(menuName = "Game/Economy Config")]
public class EconomyConfig : ScriptableObject
{
    [Header("Sources")]
    public int metersPerCoin = 10;   // step
    public int coinsPerStep = 1;     // coins for each step
    public int dailyGift = 24;
    public int adReward = 10;
    public int questReward = 20;

    [Header("Upgrade, the big sink")]
    public int upgradeBaseCost = 40;
    public float upgradeGrowth = 1.35f;
    public int upgradeMaxLevel = 10;
    public int boosterPrice = 100;


    // Price of the next upgrade: base × growth, once per level bought. Same as the sheet.
    public int UpgradeCost(int level) =>
        Mathf.RoundToInt(upgradeBaseCost * Mathf.Pow(upgradeGrowth, level));
}
