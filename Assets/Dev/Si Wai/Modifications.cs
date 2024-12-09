using UnityEngine;

public class Modifications : MonoBehaviour
{
    // this is for testing
    [ContextMenu("increase maxHealth")]
    public void IncreaseMaxHealth() {
        var maxHealth = GlobalReference.PermanentPlayerStatistic.Get<string>("maxHealth");
        Debug.Log($"maxHealth before adding: {maxHealth}");

        GlobalReference.PermanentPlayerStatistic.MaxHealth.RemoveModifier("maxHealth");
        var maxHealth1 = GlobalReference.PermanentPlayerStatistic.Get<string>("maxHealth");
        Debug.Log($"maxHealth after adding: {maxHealth1}");
    }
}