
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GlobalReward", menuName = "GlobalReward")]
public class GlobalReward : ScriptableObject
{
    public Reward CrumbReward = new("30 crumbs up", 35.0f, 30f);
    public Reward MaxHealthReward = new("Max health up", 20.0f, 0.5f);
    public Reward RandomUpgradeReward = new("Random upgrade choice", 45.0f, 0f);

    public List<Reward> List() {
        List<Reward> rewards = new();

        if (CrumbReward != null) rewards.Add(CrumbReward);
        if (MaxHealthReward != null) rewards.Add(MaxHealthReward);
        if (RandomUpgradeReward != null) rewards.Add(RandomUpgradeReward);

        return rewards;
    }

    public int Count() => List().Count;
}