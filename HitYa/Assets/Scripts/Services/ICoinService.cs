public interface ICoinService
{
    int Balance { get; }
    void AddCoins(int amount);
    bool TryPurchase(int amount, string ownershipKey);
    int AwardForDistance(int distance);
}
