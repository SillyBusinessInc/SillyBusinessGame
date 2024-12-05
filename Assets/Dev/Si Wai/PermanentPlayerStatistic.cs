
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[System.Serializable]
public class PermanentPlayerStatistic : SaveSystem
{
    public PermanentStatistic Speed;
    public PermanentStatistic JumpForce;
    public PermanentStatistic MaxHealth;
    private int crumbs;
    public int Crumbs
    {
        get => crumbs;
        set => crumbs = value > 0 ? value : 0;
    }
    public PermanentStatistic AttackSpeedMultiplier;
    public PermanentStatistic AttackDamageMultiplier;
    public PermanentStatistic DodgeCooldown;
    public PermanentStatistic DoubleJumpsCount;
    protected override string Prefix => "permanentPlayerStatistic";

    void Setup() {
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

        List<PermanentStatistic> Statistics = new() {
            Speed,
            JumpForce,
            MaxHealth,
            AttackSpeedMultiplier,
            AttackDamageMultiplier,
            DodgeCooldown,
            DoubleJumpsCount
        };

        foreach(var stat in Statistics) {
            // using json strings to save the stats
            Add(stat.Param, stat.SerializeModifications());
        }

        Add("crumbs", Crumbs);
    }

    public void SaveCrumbs() {
        Set("crumbs", Crumbs);
        SaveAll();
        GlobalReference.AttemptInvoke(Events.CRUMBS_CHANGED);
    }
}