
using System;
using UnityEngine;

[Serializable]
public class PermanentPlayerStatistic : SecureSaveSystem
{
    public PermanentStatistic Speed;
    public PermanentStatistic JumpForce;
    public PermanentStatistic MaxHealth;
    public PermanentStatistic AttackSpeedMultiplier;
    public PermanentStatistic AttackDamageMultiplier;
    public PermanentStatistic DodgeCooldown;
    public PermanentStatistic DoubleJumpsCount;
    protected override string Prefix => "PermanentPlayerStatistic";

    private void Setup() {
        Speed = new("speed", this);
        JumpForce = new("jumpForce", this);
        MaxHealth = new("maxHealth", this);
        AttackSpeedMultiplier = new("attackSpeedMultiplier", this);
        AttackDamageMultiplier = new("attackDamageMultiplier", this);
        DodgeCooldown = new("dodgeCooldown", this);
        DoubleJumpsCount = new("doubleJumpsCount", this);
    }

    public override void Init() {
        Setup();
        
        var stats = new[] {
            Speed, JumpForce, MaxHealth, AttackSpeedMultiplier, AttackDamageMultiplier, DodgeCooldown, DoubleJumpsCount
        };
        
        // these are just default values
        foreach (var stat in stats) {
            Add(stat.Param, "");
        }
        Add("crumbs", 0);
    }

     public void Generate() {
        var stats = new[] {
            Speed, JumpForce, MaxHealth, AttackSpeedMultiplier, AttackDamageMultiplier, DodgeCooldown, DoubleJumpsCount
        };

        // putting the saved values in the multipliers and modifiers list
        foreach (var stat in stats) {
            stat.DeserializeModifications(Get<string>(stat.Param));
        }
    }

    public void ModifyCrumbs(int delta) {
        var crumbs = Get<int>("crumbs") + delta;
        Set("crumbs", crumbs);
        SaveAll();
        GlobalReference.AttemptInvoke(Events.CRUMBS_CHANGED);
    }
}