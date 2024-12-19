using UnityEngine;

public class Coin : Collectable
{
    public int pointValue = 10; // Value of the coin

    public override void OnCollect()
    {
        var playerStats = GlobalReference.GetReference<PlayerReference>().Player.playerStatistic;
        playerStats.Crumbs += pointValue;

        Debug.Log($"Coin collected! Total Crumbs: {playerStats.Crumbs}");
    }
}
