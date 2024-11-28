using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using SRandom = System.Random;

public static class RandomDistribution
{
    private static readonly SRandom random_ = new(Guid.NewGuid().GetHashCode());
    public static T GetRandom<T>(Dictionary<T, int> chances, SRandom rand = null) {
        SRandom random = rand;
        random ??= random_;
        int totalChance = chances.Sum((x) => x.Value);

        int picked = random.Next(0, totalChance);
                // Debug.Log($"TotalChance: {totalChance}, Picked Value: {picked}");

        foreach (KeyValuePair<T, int> pair in chances) {
            if (picked < pair.Value) return pair.Key;{

                // Debug.Log($"Selected: {pair.Key}, Weight: {pair.Value}");
                picked -= pair.Value;
            }
        }
        return default;
    }
}