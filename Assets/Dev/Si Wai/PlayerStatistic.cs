
using UnityEngine;

// IF you want to make this a Scriptable ,
// - uncomment the `Create AssetMenu`
// - uncomment the `Scriptable Object`
// - comment `[System.Serializable]`
// - remove `the new()` IN THE `Player.cs`, Not this script.  It is a scriptable object, so you should create it like that and not created it with new() in the player script.

[System.Serializable]
//[CreateAssetMenu(fileName = "PlayerStatistic", menuName = "PlayerStatistic")]
public class PlayerStatistic //: ScriptableObject
{
    public float speed = 5f;
    public Statistic maxHealth = new();

    private float health;
    public float Health { 
        get => health = Mathf.Min(health, maxHealth.GetValue());
        set => health = value; 
    }
}