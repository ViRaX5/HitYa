using UnityEngine;

public static class GameSystems
{
    public static ICoinService Coins
    {
        get => Services.Coins;
        set => Services.Coins = value;
    }
}
