using System;

public class CoinService : ICoinService
{
    private const string CoinsKey = "coins";
    private readonly ISaveService save;

    public CoinService(ISaveService saveService)
    {
        save = saveService ?? throw new ArgumentNullException(nameof(saveService));
    }

    public int Balance => save.GetInt(CoinsKey);

    public void AddCoins(int amount)
    {
        if (amount <= 0) return;
        save.SetInt(CoinsKey, checked(Balance + amount));
    }

    public bool TryPurchase(int amount, string ownershipKey)
    {
        int balance = Balance;
        if (amount <= 0 || amount > balance || save.GetInt(ownershipKey) != 0) return false;

        save.SetInts(CoinsKey, balance - amount, ownershipKey, 1);
        return true;
    }

    public int AwardForDistance(int distance)
    {
        EconomyConfig economy = Services.Config.Economy;
        int reward = distance / economy.metersPerCoin * economy.coinsPerStep;
        AddCoins(reward);
        return reward;
    }
}
