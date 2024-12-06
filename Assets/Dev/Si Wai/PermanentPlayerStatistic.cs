
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PermanentPlayerStatistic : SecureSaveSystem
{
    protected override string Prefix => "permanentPlayerStatistic";

    public override void Init() {
        Add("speed", new PermanentStatistic("speed", this));
        Add("jumpForce", new PermanentStatistic("jumpForce", this));
        Add("maxHealth", new PermanentStatistic("maxHealth", this));
        Add("attackSpeedMultiplier", new PermanentStatistic("attackSpeedMultiplier", this));
        Add("attackDamageMultiplier", new PermanentStatistic("attackDamageMultiplier", this));
        Add("dodgeCooldown", new PermanentStatistic("dodgeCooldown", this));
        Add("doubleJumpsCount", new PermanentStatistic("doubleJumpsCount", this));
        Add("crumbs", 0);
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