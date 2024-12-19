using System.Collections.Generic;
using UnityEngine;

public class InteractableUpgradeUI : Interactable
{
    public override void TriggerInteraction(PlayerInteraction interactor)
    {

        List<UpgradeOption> upgradeOptions = new();

        List<UpgradeOption> options = GlobalReference.GetReference<RewardManagerReference>().GetRandomUpgrades(3);
        Debug.LogWarning("options:" + options[0].name + " " + options[1].name + " " + options[2].name);

        options.ForEach((o) => upgradeOptions.Add(o));

        upgradeOptions.ForEach((u) => Debug.LogWarning(u.name));
        GlobalReference.GetReference<AudioManager>().PlaySFX(GlobalReference.GetReference<AudioManager>().powerUpPickUp);
        GlobalReference.GetReference<UpgradeOptions>().options = upgradeOptions;
        GlobalReference.GetReference<UpgradeOptions>().ShowOptions();

        base.TriggerInteraction(interactor);
        IsDisabled = true;
    }
}
