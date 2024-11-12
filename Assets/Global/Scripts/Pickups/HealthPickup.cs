using UnityEngine;

public class HealthPickup : PickupBase
{
    protected override void OnTrigger()
    {
        // GlobalReference.Get<PlayerReference>().GetComponent<Player>().Heal(5);
        
        GlobalReference.AttemptInvoke(Events.PICKUP_COLLECTED);
    }
}
