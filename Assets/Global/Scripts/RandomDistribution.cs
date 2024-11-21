using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using SRandom = System.Random;

public class RandomDistribution
{
    private readonly SRandom random_ = new();
    public T GetRandom<T>(Dictionary<T, int> chances, SRandom rand = null) {
        SRandom random = rand;
        random ??= random_;
        
        int totalChance = chances.Sum((x) => x.Value);

        int picked = random.Next(0, totalChance);

        foreach (KeyValuePair<T, int> pair in chances) {
            if (picked < pair.Value) return pair.Key;
            picked -= pair.Value;
        }
        return default;
    }
}