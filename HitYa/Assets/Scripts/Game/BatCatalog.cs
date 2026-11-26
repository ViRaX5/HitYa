using System;

// Shop prices and saved bat IDs shared by Shop and Level.
public static class BatCatalog
{
    public const int ClassicWood = 0;
    public const int GoldenSlugger = 3;
    public const string EquippedKey = "shop.bat.equipped";

    // Shop order for prices: Steel Bat, Neon Zap, Candy Cane, Flame Bat, Golden Slugger.
    // The first one costs UpgradeCost(0), the next UpgradeCost(1), and so on.
    private static readonly int[] priceOrder = { 1, 2, 4, 5, GoldenSlugger };

    private static readonly string[] names = { "Classic Wood", "Steel Bat", "Neon Zap", "Golden Slugger", "Candy Cane", "Flame Bat" };
    private static readonly string[] descriptions = { "Reliable all-rounder", "Polished steel finish", "Electric aqua finish", "", "Pink and white stripes", "Orange flame finish" };
    private static readonly BatRarity[] rarities =
    {
        BatRarity.Regular, BatRarity.Regular, BatRarity.Rare,
        BatRarity.Legendary, BatRarity.Rare, BatRarity.Legendary
    };

    public static int Count => names.Length;
    public static int Equipped => Services.Save?.GetInt(EquippedKey, ClassicWood) ?? ClassicWood;
    public static string Name(int bat) => names[bat];
    // A bat with a hit power bonus shows it (from EconomyConfig), the others show their text.
    public static string Description(int bat) => PowerBonus(bat) > 0 ? "+" + PowerBonus(bat) + "% hit power" : descriptions[bat];
    public static BatRarity Rarity(int bat) => rarities[bat];
    public static string OwnershipKey(int bat) => "shop.bat.owned." + bat;
    public static bool Owns(int bat) => bat == ClassicWood || Services.Save?.GetInt(OwnershipKey(bat)) == 1;

    public static int Price(int bat)
    {
        EconomyConfig economy = Services.Config?.Economy;
        int place = Array.IndexOf(priceOrder, bat);
        if (place < 0 || economy == null) return 0;

        return economy.UpgradeCost(place);
    }

    // Hit power bonus in percent, from EconomyConfig (0 for the Classic Wood or if the config is missing).
    public static int PowerBonus(int bat)
    {
        int[] bonuses = Services.Config?.Economy?.batPowerBonusPercent;
        if (bonuses == null || bat < 0 || bat >= bonuses.Length) return 0;

        return bonuses[bat];
    }

    // The half-price deal uses the catalog price as the amount paid.
    public static long RegularPrice(int bat) => bat == GoldenSlugger ? 2L * Price(bat) : Price(bat);
}
