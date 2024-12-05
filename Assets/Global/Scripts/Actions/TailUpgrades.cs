using UnityEngine;

[CreateAssetMenu(menuName = "Actions/TailUpgrades")]
public class TailUpgrades : OneParamAction
{
    public string actionName = "TailUpgrades";
    public override void InvokeAction(ActionMetaData _, string param)
    {
        Tail tail = GlobalReference
            .GetReference<PlayerReference>()
            .GetComponent<Player>()
            .Tail;
        if (actionName == "WaffleQuake")
        {
            tail.WaffleQuake();
        }
        if (actionName == "DoubleTap")
        {
            tail.DoubleTap();
        }
    }

}
