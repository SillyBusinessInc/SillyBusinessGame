using UnityEngine;

public class PickupHealth : PickupBase
{
    protected override void OnTrigger()
    {
        GlobalReference.Player.Heal(5);
    }
}
