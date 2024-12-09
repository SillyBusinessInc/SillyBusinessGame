
using System;
using UnityEngine;

/*
    !!IMPORTANT
    To add a new statistic, add a new field that is of type PermanentStatistic
    Then in the Setup method, instantiate it with the name of the stat and add 'this' as the second parameter to being able to save the stat
    In the init() and Genrate() methods you add the new field in the stats list

    If the field will be a string, float, bool or int you can just use Add(statname, defaultValue); in the Init() method

    Then, in PlayerStatistic.cs you add a new field, but instead of PermanentStatistic, CurrentStatistic will be the type
    In the Generate() method in PlayerStatistic.cs you need to instatiate. The 1st parameter is the baseValue and the 2nd parameter is the PermanemtStatistic
    To call the PermanentStatistic you call it like GlobalReference.PermanentPlayerStatistic.fieldname

    To get the crumbs, you can to call
    GlobalReference.PermanentPlayerStatistic.Get<int>("crumbs")

    For the other statistics, you are not able to get the permanentStatistics directly. You need to instantiate the PlayerStatistic class. 
    Then you can call PlayerStatistic.GetValue() which will give you the value with all the added multipliers and modifiers including the permanent ones
*/

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