using UnityEngine;

public class HealthPickup : PickupBase
{
    protected override void OnTrigger()
    {
        GlobalReference.Player.Heal(5);
    }
}
