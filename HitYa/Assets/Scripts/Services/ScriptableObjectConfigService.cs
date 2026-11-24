// ScriptableObjectConfigService.cs: today's how, the asset from the Inspector
public class ScriptableObjectConfigService : IConfigService
{
    public EconomyConfig Economy { get; }
    public ScriptableObjectConfigService(EconomyConfig economy) => Economy = economy;
}