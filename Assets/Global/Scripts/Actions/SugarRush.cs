
using UnityEngine;
[CreateAssetMenu(menuName = "Actions/SugarRush")]
public class SugarRush : ActionScriptableObject
{
    [SerializeField] private string actionName = "SugarRush";
    public override void InvokeAction(string param)
    {
        GlobalReference
            .GetReference<PlayerReference>()
            .GetComponent<Player>().playerStatistic.Speed.AddModifier("SugarRush", 1.1f);
    }

}
