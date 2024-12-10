using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableUpgradeUI : Interactable
{
    public override void TriggerInteraction(PlayerInteraction interactor)
    {
        List<UpgradeOption> upgradeOptions = new() {
            GlobalReference.GetReference<RewardManagerReference>().GetRandomUpgrade(),
            GlobalReference.GetReference<RewardManagerReference>().GetRandomUpgrade(),
            GlobalReference.GetReference<RewardManagerReference>().GetRandomUpgrade()
        };

        GlobalReference.GetReference<UpgradeOptions>().options = upgradeOptions;
        GlobalReference.GetReference<UpgradeOptions>().ShowOptions();
        
        base.TriggerInteraction(interactor);
        IsDisabled = true;
    }
}
