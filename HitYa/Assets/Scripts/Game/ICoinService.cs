public interface ICoinService
{
    int Balance { get; }
    void AddCoins(int amount);
    int AwardForDistance(int distance);
}
