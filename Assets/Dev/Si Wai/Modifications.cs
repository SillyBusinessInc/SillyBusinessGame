using UnityEngine;
using TMPro;

public class Modifications : MonoBehaviour
{
    void Start() 
    {
        // this is for testing
        Debug.Log($"crumbs: {GlobalReference.PermanentPlayerStatistic.Get<int>("crumbs")}");
        Debug.Log($"health: {GlobalReference.PermanentPlayerStatistic.Get<float>("health")}");
        GlobalReference.SubscribeTo(Events.CRUMBS_CHANGED, ChangeCrumbs);
        GlobalReference.SubscribeTo(Events.HEALTH_CHANGED, ChangeMaxHealth);
    
    }

    void ChangeMaxHealth() {
        Debug.Log($"new health: {GlobalReference.PermanentPlayerStatistic.Get<float>("health")}");
    }

    void ChangeCrumbs() {
        Debug.Log($"new crumbs: {GlobalReference.PermanentPlayerStatistic.Get<int>("crumbs")}");
    }
}