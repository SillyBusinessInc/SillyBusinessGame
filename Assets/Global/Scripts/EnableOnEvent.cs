using System.Collections.Generic;
using UnityEngine;

public class EnableOnEvent : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToEnable;
    [SerializeField] private Events enableEvent;
    
    void Start()
    {
        foreach (var obj in objectsToEnable)
        {
            obj.SetActive(false);
        }
        
        GlobalReference.SubscribeTo(this.enableEvent, EnableAll);
    }

    private void EnableAll()
    {
        foreach (var obj in objectsToEnable)
        {
            obj.SetActive(true);
        }
    }
}
