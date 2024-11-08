using UnityEngine;

public class PickupHealth : PickupBase
{
    protected override void OnTrigger()
    {
        // GlobalReference.Get<PlayerReference>().GetComponent<Player>().Heal(5);
    }
}
