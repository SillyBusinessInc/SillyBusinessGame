using UnityEngine;

public class MaxHealthReward : Reward
{
    [SerializeField] private readonly float reward;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Title = "Max health up";
        Weight = 20.0f;
    }

    public override void ActivateReward(Player player) {
        player.playerStatistic.MaxHealth.AddModifier("maxHealthReward", reward);
    }
}