// Shop prices and saved bat IDs shared by Shop and Level.
public static class BatCatalog
{
    public const int ClassicWood = 0;
    public const int GoldenSlugger = 3;
    public const int FlameBat = 5;
    public const string EquippedKey = "shop.bat.equipped";

    private static readonly string[] names = { "Classic Wood", "Steel Bat", "Neon Zap", "Golden Slugger", "Candy Cane", "Flame Bat" };
    private static readonly string[] descriptions = { "Reliable all-rounder", "Polished steel finish", "Electric aqua finish", "+25% hit power", "Pink and white stripes", "Orange flame finish" };
    private static readonly int[] prices = { 0, 250, 800, 600, 1500, 60 };

    public static int Count => names.Length;
    public static int Equipped => Services.Save?.GetInt(EquippedKey, ClassicWood) ?? ClassicWood;
    public static string Name(int bat) => names[bat];
    public static string Description(int bat) => descriptions[bat];
    public static int Price(int bat) => prices[bat];
    public static string OwnershipKey(int bat) => "shop.bat.owned." + bat;
    public static bool Owns(int bat) => bat == ClassicWood || Services.Save?.GetInt(OwnershipKey(bat)) == 1;
}
