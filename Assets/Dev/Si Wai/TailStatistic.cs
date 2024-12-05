using UnityEngine;

// IF you want to make this a Scriptable ,
// - uncomment the `Create AssetMenu`
// - uncomment the `Scriptable Object`
// - comment `[System.Serializable]`
// - remove `the new()` IN THE `Player.cs`, Not this script.  It is a scriptable object, so you should create it like that and not created it with new() in the player script.

[System.Serializable]
//[CreateAssetMenu(fileName = "PlayerStatistic", menuName = "PlayerStatistic")]
public class TailStatistic //: ScriptableObject
{
    public Statistic slamObjectSize = new(1.0f);
    public Statistic increaseTailSpeed = new(1.0f);
    public Statistic comboResetTime = new(2.0f);
    public Statistic leftTailDamage = new(10.0f);
    public Statistic rightTailDamage = new(15.0f);
    public Statistic flipTailDamage = new(20.0f);

    public Statistic leftTailCooldown = new(0.0f);
    public Statistic rightTailCooldown = new(0.0f);
    public Statistic flipTailCooldown = new(0.0f);
}
