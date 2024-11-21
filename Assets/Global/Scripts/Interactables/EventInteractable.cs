using UnityEngine;

public class EventInteractable : Interactable
{
    [SerializeField] private Events eventje = Events.PICKUP_COLLECTED;
    [SerializeField] private Events eventje2 = Events.NONE;
    [SerializeField] private Events eventje3 = Events.NONE;
    
    public override void OnInteract()
    {
        GlobalReference.AttemptInvoke(eventje);
        GlobalReference.AttemptInvoke(eventje2);
        GlobalReference.AttemptInvoke(eventje3);
    }
}
