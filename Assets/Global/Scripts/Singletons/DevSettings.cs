public class DevSettings : SaveSystem
{
    protected override string Prefix => "dev_settings";

    public override void Init() {
        // room layout generation options
        Add("maxObjectPerDepth", 3);
        Add("minBranchCount", 2);
        Add("maxBranchCount", 3);
        Add("targetDepth", 5);
        Add("bonusChance", 20);
        Add("seed", -1);
        Add("shopDepthOverride", -2);
    }
}