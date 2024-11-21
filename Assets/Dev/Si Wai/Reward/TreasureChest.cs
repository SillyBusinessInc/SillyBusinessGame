using UnityEngine;

public class TreasureChest : Interactable
{
    public Reward Reward;

    public override void OnInteract() {
        Player player = GlobalReference.GetReference<PlayerReference>().Player;
        Reward.ActivateReward(player);
        this.gameObject.SetActive(false);
    }
}
