using UnityEngine;

public class RandomUpgradeReward : Reward
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Title = "Random upgrade choice";
        Weight = 45.0f;
    }

    public override void ActivateReward(Player player) {
        Debug.Log("U got the random upgrade choice reward!");
    }
}