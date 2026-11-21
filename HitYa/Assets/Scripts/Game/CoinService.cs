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

    public int AwardForDistance(int distance)
    {
        int reward = 0;
        if (distance >= 500) reward += 5;
        if (distance >= 1000) reward += 10;
        if (distance >= 1500) reward += 20;
        AddCoins(reward);
        return reward;
    }
}
