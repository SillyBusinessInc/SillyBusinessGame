using UnityEngine;

[CreateAssetMenu(menuName = "Actions/StarFruitPlatinum")]
public class StarFruitPlatinum : ActionScriptableObject
{
    [SerializeField]
    private string actionName = "StarFruitPlatinum";

    public float increaseAttackSpeedMultiplier = 10f;

    public override void InvokeAction(string param)
    {
        GlobalReference
            .GetReference<PlayerReference>()
            .GetComponent<Player>().
            playerStatistic.AttackSpeedMultiplier.AddMultiplier(actionName, increaseAttackSpeedMultiplier, true);
    }
}
