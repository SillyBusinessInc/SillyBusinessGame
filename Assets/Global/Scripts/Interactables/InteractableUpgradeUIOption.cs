using System.Collections.Generic;
using UnityEngine;

public class InteractableUpgradeUIOption : Interactable
{
    public override void TriggerInteraction(PlayerInteraction interactor)
    {
        var UpgradeOptions = GlobalReference.GetReference<UpgradeOptions>();
        UpgradeOptions.option = GlobalReference.GetReference<RewardManagerReference>().GetRandomUpgrade();
        UpgradeOptions.ShowOptions();
        
        base.TriggerInteraction(interactor);
        IsDisabled = true;
    }
}
