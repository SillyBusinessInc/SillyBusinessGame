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
        if (randomNumber < thresholds[0]) {
            return Rewards[0];
        } else if (randomNumber < thresholds[1]) {
            return Rewards[1];
        } else {
            return Rewards[2];
        }
    }
}
