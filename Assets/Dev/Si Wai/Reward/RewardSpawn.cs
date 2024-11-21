using System.Collections.Generic;
using UnityEngine;

public class RewardSpawn : MonoBehaviour
{
    public GlobalReward Rewards;
    [SerializeField] private TreasureChest TreasureChest;
    private Reward Reward;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Spawn reward")]
    void SpawnReward() {
        Reward = GetRandomReward();
        TreasureChest.Reward = Reward;
        TreasureChest.gameObject.SetActive(true);
    }

    Reward GetRandomReward() {
        float[] thresholds = new float[Rewards.Count()];
        float totalWeight = 0;
        for (int i = 0; i < thresholds.Length; i++) {
            totalWeight += Rewards.List()[i].Weight;
            thresholds[i] = totalWeight;
        }

        int randomNumber = Random.Range(0, 100);
        if (randomNumber < thresholds[0]) {
            return Rewards.CrumbReward;
        } else if (randomNumber < thresholds[1]) {
            return Rewards.MaxHealthReward;
        } else {
            return Rewards.RandomUpgradeReward;
        }
    }
}
