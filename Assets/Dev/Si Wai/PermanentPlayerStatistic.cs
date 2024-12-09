
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
        
        Add("speed", "");
        Add("jumpForce", "");
        Add("maxHealth", "");
        Add("attackSpeedMultiplier", "");
        Add("attackDamageMultiplier", "");
        Add("dodgeCooldown", "");
        Add("doubleJumpsCount", "");
        Add("crumbs", 0);
    }

    // putting the saved values to the 
    public void Generate() {
        Speed.DeserializeModifications(Get<string>("speed"));
        JumpForce.DeserializeModifications(Get<string>("jumpForce"));
        MaxHealth.DeserializeModifications(Get<string>("maxHealth"));
        AttackSpeedMultiplier.DeserializeModifications(Get<string>("attackSpeedMultiplier"));
        AttackDamageMultiplier.DeserializeModifications(Get<string>("attackDamageMultiplier"));
        DodgeCooldown.DeserializeModifications(Get<string>("dodgeCooldown"));
        DoubleJumpsCount.DeserializeModifications(Get<string>("doubleJumpsCount"));
    }

    public void IncreaseCrumbs(int reward) {
        int crumbs = Get<int>("crumbs");
        crumbs += reward;
        SaveCrumbs(crumbs);
    }

    public void DecreaseCrumbs(int price) {
        int crumbs = Get<int>("crumbs");
        crumbs -= price;
        SaveCrumbs(crumbs);
    }

    private void SaveCrumbs(int crumbs) {
        Set("crumbs", crumbs);
        SaveAll();
        GlobalReference.AttemptInvoke(Events.CRUMBS_CHANGED);
    }
}