using UnityEngine;

[CreateAssetMenu(menuName = "Actions/StarFruitPlatinum")]
public class StarFruitPlatinum : ActionScriptableObject
{
    [SerializeField]
    private string actionName = "StarFruitPlatinum";

    public override void InvokeAction(string param)
    {
        Debug.Log("StarFruitPlatinum");
        GlobalReference
            .GetReference<PlayerReference>()
            .GetComponent<Player>()
            .Tail.tailStatistic.increaseAttackSpeed.AddMultiplier(actionName, 10f, true);
    }
}
