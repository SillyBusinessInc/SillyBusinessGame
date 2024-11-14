using System.Collections.Generic;
using UnityEngine;

public class RewardSpawn : MonoBehaviour
{
    public List<Reward> Rewards;
    public Reward Reward;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Reward = GetRandomReward();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Reward GetRandomReward() {
        float[] thresholds = new float[Rewards.Count];
        for (int i = 0; i < thresholds.Length; i++) {
            float totalWeight = 0;
            for (int j = 0; j < i; j++) {
                totalWeight += Rewards[i].Weight;
            }
            thresholds[i] = totalWeight;
        }

        int randomNumber = Random.Range(0, 100);
        if (randomNumber < thresholds[0])
        {
            return Rewards[0];  // 35% chance for the first item
        }
        else if (randomNumber < thresholds[1])
        {
            return Rewards[1];  // 20% chance for the second item (cumulative 55%)
        }
        else if (randomNumber < thresholds[2])
        {
            return Rewards[2];  // 25% chance for the third item (cumulative 80%)
        }
        else
        {
            return Rewards[3];  // 20% chance for the fourth item (cumulative 100%)
        }
    }
}
