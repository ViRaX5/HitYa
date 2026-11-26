// Shop prices and saved bat IDs shared by Shop and Level.
public static class BatCatalog
{
    public const int ClassicWood = 0;
    public const int GoldenSlugger = 3;
    public const string EquippedKey = "shop.bat.equipped";

    private static readonly string[] names = { "Classic Wood", "Steel Bat", "Neon Zap", "Golden Slugger", "Candy Cane", "Flame Bat" };
    private static readonly string[] descriptions = { "Reliable all-rounder", "Polished steel finish", "Electric aqua finish", "+25% hit power", "Pink and white stripes", "Orange flame finish" };
    private static readonly BatRarity[] rarities =
    {
        BatRarity.Regular, BatRarity.Regular, BatRarity.Rare,
        BatRarity.Legendary, BatRarity.Rare, BatRarity.Legendary
    };

    public static int Count => names.Length;
    public static int Equipped => Services.Save?.GetInt(EquippedKey, ClassicWood) ?? ClassicWood;
    public static string Name(int bat) => names[bat];
    public static string Description(int bat) => descriptions[bat];
    public static BatRarity Rarity(int bat) => rarities[bat];
    public static string OwnershipKey(int bat) => "shop.bat.owned." + bat;
    public static bool Owns(int bat) => bat == ClassicWood || Services.Save?.GetInt(OwnershipKey(bat)) == 1;

    public static int Price(int bat)
    {
        EconomyConfig economy = Services.Config?.Economy;
        if (bat <= ClassicWood || bat >= Count || economy == null) return 0;

        return economy.UpgradeCost(bat - 1);
    }

    // The half-price deal uses the catalog price as the amount paid.
    public static long RegularPrice(int bat) => bat == GoldenSlugger ? 2L * Price(bat) : Price(bat);
}
