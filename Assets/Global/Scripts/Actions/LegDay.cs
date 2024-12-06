using UnityEngine;
[CreateAssetMenu(menuName = "Actions/LegDay")]
public class LegDay : ActionScriptableObject
{
    [SerializeField] private string actionName = "LegDay";
    public override void InvokeAction(string param)
    {
        GlobalReference
            .GetReference<PlayerReference>()
            .GetComponent<Player>().playerStatistic.DoubleJumpsCount.AddModifier(actionName, 1);
    }

}
