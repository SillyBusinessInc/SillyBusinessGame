
using UnityEngine;
[System.Serializable]
public class PermanentPlayerStatistic : SaveSystem
{
    public PermanentStatistic Speed = new();
    public PermanentStatistic JumpForce = new();
    public PermanentStatistic MaxHealth = new();
    private float health;
    public float Health {
        get => health;
        set => health = value > 0 ? value : 0;
    }
    private int crumbs;
    public int Crumbs
    {
        get => crumbs;
        set => crumbs = value > 0 ? value : 0;
    }
    public PermanentStatistic AttackSpeedMultiplier = new();
    public PermanentStatistic AttackDamageMultiplier = new();
    public PermanentStatistic DodgeCooldown = new();
    public PermanentStatistic DoubleJumpsCount = new();
    protected override string Prefix => "permanentPlayerStatistic";

    public override void Init() {
        // using json strings to save the stats
        string json;

        json = JsonUtility.ToJson(Speed.ListWithModifications());
        Add("speed", json);

        json = JsonUtility.ToJson(JumpForce.ListWithModifications());
        Add("jumpForce", json);

        json = JsonUtility.ToJson(MaxHealth.ListWithModifications());
        Add("maxHealth", json);

        Add("health", Health);
        Add("crumbs", Crumbs);

        json = JsonUtility.ToJson(AttackSpeedMultiplier.ListWithModifications());
        Add("attackSpeedMultiplier", json);

        json = JsonUtility.ToJson(AttackDamageMultiplier.ListWithModifications());
        Add("attackDamageMultiplier", json);

        json = JsonUtility.ToJson(DodgeCooldown.ListWithModifications());
        Add("dodgeCooldown", json);

        json = JsonUtility.ToJson(DoubleJumpsCount.ListWithModifications());
        Add("doubleJumpsCount", json);
    }
}