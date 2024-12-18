using UnityEngine;

public class Coin : Collectable
{
    public int pointValue = 10; // Value of the coin

    public override void OnCollect()
    {
    var playerStats = GlobalReference.GetReference<PlayerReference>().Player.playerStatistic;

    Debug.Log($"Current Crumbs: {playerStats.Crumbs}");

    // Add pointValue to Crumbs (the setter will validate the value)
    playerStats.Crumbs += pointValue;

    Debug.Log($"Coin collected! Total Crumbs: {playerStats.Crumbs}");
    Debug.Log($"Coin collected! +{pointValue} points");
    }
}
