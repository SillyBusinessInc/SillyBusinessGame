using UnityEngine;

public class EventPickup : PickupBase
{
    
    [SerializeField] private Events eventje = Events.PICKUP_COLLECTED;
    [SerializeField] private Events eventje2 = Events.NONE;
    [SerializeField] private Events eventje3 = Events.NONE;
    protected override void OnTrigger()
    {
        GlobalReference.AttemptInvoke(eventje);
        GlobalReference.AttemptInvoke(eventje2);
        GlobalReference.AttemptInvoke(eventje3);
    }
}
