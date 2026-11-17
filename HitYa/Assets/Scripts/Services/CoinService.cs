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
        int reward = distance >= 1500 ? 20 : distance >= 1000 ? 10 : distance >= 500 ? 5 : 0;
        AddCoins(reward);
        return reward;
    }
}
