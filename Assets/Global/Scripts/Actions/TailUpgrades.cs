using UnityEngine;

[CreateAssetMenu(menuName = "Actions/TailUpgrades")]
public class TailUpgrades : ActionScriptableObject
{
    public string actionName = "TailUpgrades";
    public override void InvokeAction(string param)
    {
        Tail tail = GlobalReference
            .GetReference<PlayerReference>()
            .GetComponent<Player>()
            .Tail;
        if(actionName == "WaffleQuake")
        {
            tail.WaffleQuake();
        }
        if(actionName == "DoubleTap")
        {
            tail.DoubleTap();
        }
    }
    
}
