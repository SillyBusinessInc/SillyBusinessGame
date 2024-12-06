using UnityEngine;
[CreateAssetMenu(menuName = "Actions/GhostPepper")]
public class GhostPepper : OneParamAction
{
    [SerializeField] private string actionName = "GhostPepper";

    public float damageMultiplier = 1.1f;
    public override void InvokeAction(ActionMetaData _, string param)
    {
        GlobalReference
            .GetReference<PlayerReference>()
            .GetComponent<Player>().playerStatistic.AttackDamageMultiplier.AddMultiplier(actionName, damageMultiplier, true);
    }

}
