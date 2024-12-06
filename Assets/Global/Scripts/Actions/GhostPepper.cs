using UnityEngine;
[CreateAssetMenu(menuName = "Actions/GhostPepper")]
public class GhostPepper : ActionScriptableObject
{
    [SerializeField] private string actionName = "GhostPepper";
    public override void InvokeAction(string param)
    {
        GlobalReference
            .GetReference<PlayerReference>()
            .GetComponent<Player>().Tail.tailStatistic.increaseDamage.AddMultiplier(actionName, 1.1f, true);
    }

}
