using UnityEngine;

public class Modifications : MonoBehaviour
{
    // this is for testing
    [ContextMenu("increase maxHealth")]
    public void IncreaseMaxHealth() {
        var maxHealth = GlobalReference.PermanentPlayerStatistic.Get<string>("maxHealth");
        Debug.Log($"maxHealth before adding: {maxHealth}");

        GlobalReference.PermanentPlayerStatistic.MaxHealth.AddModifier("maxHealth", 1f);
        var maxHealth1 = GlobalReference.PermanentPlayerStatistic.Get<string>("maxHealth");
        Debug.Log($"maxHealth after adding: {maxHealth1}");
    }

    [ContextMenu("decrease maxHealth")]
    public void DecreaseMaxHealth() {
        var maxHealth = GlobalReference.PermanentPlayerStatistic.Get<string>("maxHealth");
        Debug.Log($"maxHealth before removing: {maxHealth}");

        GlobalReference.PermanentPlayerStatistic.MaxHealth.RemoveModifier("maxHealth");
        var maxHealth1 = GlobalReference.PermanentPlayerStatistic.Get<string>("maxHealth");
        Debug.Log($"maxHealth after removing: {maxHealth1}");
    }

    [ContextMenu("crumbs up 30")]
    public void IncreaseCrumbs() {
        Debug.Log($"crumbs before adding: {GlobalReference.PermanentPlayerStatistic.Get<int>("crumbs")}");
        GlobalReference.PermanentPlayerStatistic.ModifyCrumbs(30);
        Debug.Log($"crumbs after adding: {GlobalReference.PermanentPlayerStatistic.Get<int>("crumbs")}");
    }

    [ContextMenu("crumbs down 30")]
    public void DecreaseCrumbs() {
        Debug.Log($"crumbs before removing: {GlobalReference.PermanentPlayerStatistic.Get<int>("crumbs")}");
        GlobalReference.PermanentPlayerStatistic.ModifyCrumbs(-30);
        Debug.Log($"crumbs after removing: {GlobalReference.PermanentPlayerStatistic.Get<int>("crumbs")}");
    }
}