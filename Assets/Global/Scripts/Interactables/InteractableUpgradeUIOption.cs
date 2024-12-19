using System.Collections.Generic;
using UnityEngine;

public class InteractableUpgradeUIOption : Interactable
{
    [ContextMenu("interact")] 
    public void TriggerInteractions()
    {
        Debug.Log( GlobalReference.GetReference<RewardManagerReference>());
        var UpgradeOptions = GlobalReference.GetReference<UpgradeOptions>();
        UpgradeOptions.option = GlobalReference.GetReference<RewardManagerReference>().GetRandomUpgrade();
        UpgradeOptions.ShowOption();
        IsDisabled = true;
    }

    public override void TriggerInteraction(PlayerInteraction interactor)
    {
        var UpgradeOptions = GlobalReference.GetReference<UpgradeOptions>();
        UpgradeOptions.option = GlobalReference.GetReference<RewardManagerReference>().GetRandomUpgrade();
        UpgradeOptions.ShowOption();
        
        base.TriggerInteraction(interactor);
        IsDisabled = true;
    }
}
