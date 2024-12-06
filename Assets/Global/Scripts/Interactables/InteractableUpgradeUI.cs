using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableUpgradeUI : Interactable
{
    public List<UpgradeOption> upgradeOptions;

    public override void TriggerInteraction()
    {
        GlobalReference.GetReference<UpgradeOptions>().options = upgradeOptions;
        GlobalReference.GetReference<UpgradeOptions>().ShowOptions();
    }
}
