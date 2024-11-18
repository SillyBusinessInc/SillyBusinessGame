using System.Collections.Generic;
using UnityEngine;

public class RewardSpawn : MonoBehaviour
{
    [SerializeField] private List<Reward> Rewards;
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
        // list reward: grey = max health reward, pink = crumb reward, black = random upgrade reward
        Reward = GetRandomReward();
        Reward.gameObject.SetActive(true);
    }

    Reward GetRandomReward() {
        float[] thresholds = new float[Rewards.Count];
        float totalWeight = 0;
        for (int i = 0; i < thresholds.Length; i++) {
            totalWeight += Rewards[i].Weight;
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
