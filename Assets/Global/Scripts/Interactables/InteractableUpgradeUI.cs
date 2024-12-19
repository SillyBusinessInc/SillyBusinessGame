using System.Collections.Generic;
using UnityEngine;

public class InteractableUpgradeUI : Interactable
{
    public override void TriggerInteraction(PlayerInteraction interactor)
    {
        UpgradeOption option = GlobalReference.GetReference<RewardManagerReference>().GetRandomUpgrade();

        GlobalReference.GetReference<UpgradeOptions>().option = option;
        GlobalReference.GetReference<UpgradeOptions>().ShowOption();

        base.TriggerInteraction(interactor);
        IsDisabled = true;
    }
}
