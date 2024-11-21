
using UnityEngine;

// IF you want to make this a Scriptable ,
// - uncomment the `Create AssetMenu`
// - uncomment the `Scriptable Object`
// - comment `[System.Serializable]`
// - remove `the new()` IN THE `Player.cs`, Not this script.  It is a scriptable object, so you should create it like that and not created it with new() in the player script.

//[System.Serializable]
[CreateAssetMenu(fileName = "PlayerStatistic", menuName = "PlayerStatistic")]
public class PlayerStatistic : ScriptableObject
{

    public Statistic Speed = new(15f);
    public Statistic JumpForce = new(8f);
    public Statistic MaxHealth = new(6f);
    private float health;
    public float Health { 
        get => health = Mathf.Min(health, MaxHealth.GetValue());
        set => health = value; 
    }
    public Statistic AttackSpeedMultiplier = new(1f);
    public Statistic AttackDamageMultiplier = new(1f);
    public Statistic DodgeCooldown = new(1f);
    public Statistic DoubleJumpsCount = new(2f);
}