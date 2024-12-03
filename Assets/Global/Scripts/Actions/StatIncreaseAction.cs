using UnityEngine;
using System.Reflection;

[CreateAssetMenu(menuName = "Actions/StatIncreaseAction")]
public class StatIncreaseAction : ActionScriptableObject
{
    [SerializeField] private string actionName = "Stat Increase Action";

    public override void InvokeAction(string param)
    {
        Player player = GlobalReference.GetReference<PlayerReference>().GetComponent<Player>();

        if (player == null)
        {
            Debug.LogWarning("Player not found! [StatIncreaseAction]");
            return;
        }

        PlayerStatistic stats = player.playerStatistic;

        if (stats == null)
        {
            Debug.LogWarning("PlayerStatistic not found! [StatIncreaseAction]");
            return;
        }

        // Use reflection to find and modify the field
        FieldInfo field = typeof(PlayerStatistic).GetField(param, BindingFlags.IgnoreCase | BindingFlags.Public);

        if (field != null && field.FieldType == typeof(Statistic))
        {
            Statistic stat = (Statistic)field.GetValue(stats);
            stat.AddModifier("StatIncreaseAction", 1);
        }
        else
        {
            Debug.LogWarning("$[CONFIG] Field {param} not found in PlayerStatistic");
        }
    }


}
