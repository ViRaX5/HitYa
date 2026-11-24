using UnityEngine;

// EconomyConfig.cs (Assets/Scripts/Services): every economy number in one asset
[CreateAssetMenu(menuName = "Game/Economy Config")]
public class EconomyConfig : ScriptableObject
{
    [Header("Sources")]
    public int coinsPerLevel = 10;
    public int dailyGift = 20;

    [Header("Upgrade, the big sink")]
    public int upgradeBaseCost = 50;
    public float upgradeGrowth = 1.3f;
    public int upgradeMaxLevel = 10;

    // Price of the next upgrade: base × growth, once per level bought. Same as the sheet.
    public int UpgradeCost(int level) =>
        Mathf.RoundToInt(upgradeBaseCost * Mathf.Pow(upgradeGrowth, level));
}
