using UnityEngine;

public class CrumbReward : Reward
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Title = "30 crumbs up";
        Weight = 35.0f;
    }

    public override void ActivateReward(Player player) {
        Debug.Log("U got the crumb reward!");
    }
}