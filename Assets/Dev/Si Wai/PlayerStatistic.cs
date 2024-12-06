
using UnityEngine;

// IF you want to make this a Scriptable ,
// - uncomment the `Create AssetMenu`
// - uncomment the `Scriptable Object`
// - comment `[System.Serializable]`
// - remove `the new()` IN THE `Player.cs`, Not this script.  It is a scriptable object, so you should create it like that and not created it with new() in the player script.

[System.Serializable]
//[CreateAssetMenu(fileName = "PlayerStatistic", menuName = "PlayerStatistic")]
public class PlayerStatistic
{
    // this is for the current stats of the player
    public CurrentStatistic Speed = new(10f, GlobalReference.PermanentPlayerStatistic.Get<PermanentStatistic>("speed"));
    public CurrentStatistic JumpForce = new(2f, GlobalReference.PermanentPlayerStatistic.Get<PermanentStatistic>("jumpForce"));
    public CurrentStatistic MaxHealth = new(6f, GlobalReference.PermanentPlayerStatistic.Get<PermanentStatistic>("maxHealth"));
    private float health;
    public float Health { 
        get => health = Mathf.Min(health, MaxHealth.GetValue());
        set => health = value > 0 ? value : 0;
    }
    public CurrentStatistic AttackSpeedMultiplier = new(1f, GlobalReference.PermanentPlayerStatistic.Get<PermanentStatistic>("attackSpeedMultiplier"));
    public CurrentStatistic AttackDamageMultiplier = new(1f, GlobalReference.PermanentPlayerStatistic.Get<PermanentStatistic>("attackDamageMultiplier"));
    public CurrentStatistic DodgeCooldown = new(1f, GlobalReference.PermanentPlayerStatistic.Get<PermanentStatistic>("dodgeCooldown"));
    public CurrentStatistic DoubleJumpsCount = new(2f, GlobalReference.PermanentPlayerStatistic.Get<PermanentStatistic>("doubleJumpsCount"));


}