using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/EventAction")]
public class EventAction : ActionScriptableObject
{
    [SerializeField] private List<Events> events;
    private List<Events> dynamicEvents;
    public override void InvokeAction(string param)
    {
        var eventNames = param.Split(',');
        foreach (var evName in eventNames)
        {
            
            if (System.Enum.TryParse(evName.Trim(), out Events ev))
                dynamicEvents.Add(ev);
            else
                Debug.LogError($"Event '{evName}' does not exist");
        }
        
        events.ForEach(e => GlobalReference.AttemptInvoke(e));
        dynamicEvents.ForEach(e => GlobalReference.AttemptInvoke(e));
        dynamicEvents.Clear();
    }
    
}

