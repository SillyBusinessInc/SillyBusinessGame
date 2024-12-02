
using UnityEngine;

// IF you want to make this a Scriptable ,
// - uncomment the `Create AssetMenu`
// - uncomment the `Scriptable Object`
// - comment `[System.Serializable]`
// - remove `the new()` IN THE `Player.cs`, Not this script.  It is a scriptable object, so you should create it like that and not created it with new() in the player script.

[System.Serializable]
//[CreateAssetMenu(fileName = "PlayerStatistic", menuName = "PlayerStatistic")]
public class PlayerStatistic //: ScriptableObject
{
    // this is for the current stats of the player
    public CurrentStatistic Speed = new(10f);
    public CurrentStatistic JumpForce = new(2f);
    public CurrentStatistic MaxHealth = new(6f);
    private float health;
    public float Health { 
        get => health = Mathf.Min(health, MaxHealth.GetValue());
        set => health = value > 0 ? value : 0;
    }
    public CurrentStatistic AttackSpeedMultiplier = new(1f);
    public CurrentStatistic AttackDamageMultiplier = new(1f);
    public CurrentStatistic DodgeCooldown = new(1f);
    public CurrentStatistic DoubleJumpsCount = new(2f);
}