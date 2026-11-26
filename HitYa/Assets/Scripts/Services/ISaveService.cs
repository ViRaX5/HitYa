// ISaveService.cs: the contract. What, not how.
public interface ISaveService
{
    int GetInt(string key, int fallback = 0);
    void SetInt(string key, int value);
    void SetInts(string firstKey, int firstValue, string secondKey, int secondValue);
}

