using System.Collections.Generic;
using UnityEngine;

public class InteractableUpgradeUI : Interactable
{
    public override void TriggerInteraction(PlayerInteraction interactor)
    {
        List<UpgradeOption> upgradeOptions = new() {
            GlobalReference.GetReference<RewardManagerReference>().GetRandomUpgrade(),
            GlobalReference.GetReference<RewardManagerReference>().GetRandomUpgrade(),
            GlobalReference.GetReference<RewardManagerReference>().GetRandomUpgrade()
        };

        upgradeOptions.ForEach((u) => Debug.LogWarning(u.name));

        GlobalReference.GetReference<UpgradeOptions>().options = upgradeOptions;
        GlobalReference.GetReference<UpgradeOptions>().ShowOptions();
        
        base.TriggerInteraction(interactor);
        IsDisabled = true;
    }
}
