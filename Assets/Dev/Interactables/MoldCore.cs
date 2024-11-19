using UnityEngine;

public class MoldCore : Interactable
{
    public override void OnInteract()
    {
        Debug.Log("Interacted with " + name);
        // Add custom action logic here
    }
}
