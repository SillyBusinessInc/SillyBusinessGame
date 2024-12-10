using System.Collections.Generic;
using UnityEngine;

public class RewardManagerReference : Reference
{
    public List<UpgradeOption> upgradeOptions;

    public UpgradeOption GetRandomUpgrade() {
        Dictionary<UpgradeOption, int> optionsTable = new();
        for (int i = 0; i < upgradeOptions.Count; i++) {
            optionsTable.Add(upgradeOptions[i], upgradeOptions[i].rarity);
        }

        return RandomDistribution.GetRandom(optionsTable);
    }
}
