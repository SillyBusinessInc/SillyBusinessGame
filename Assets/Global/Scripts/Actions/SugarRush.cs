using UnityEngine;

[CreateAssetMenu(menuName = "Actions/SugarRush")]
public class SugarRush : OneParamAction
{
    [SerializeField] private string actionName = "SugarRush";

    public float increaseSpeedMultiplier = 100f;
    public override void InvokeAction(ActionMetaData _,string param)
    {
        GlobalReference
            .GetReference<PlayerReference>()
            .GetComponent<Player>().playerStatistic.Speed
            .AddMultiplier(actionName, increaseSpeedMultiplier, true);
    }
}