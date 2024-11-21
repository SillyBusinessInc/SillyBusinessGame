using UnityEngine;

[System.Serializable]
public class Reward
{
    public string Title;
    public float Weight;
    public float Value;

    public Reward(string title, float weight, float value) {
        Title = title;
        Weight = weight;
        Value = value;
    }

    public void ActivateReward(Player player) {
        switch(Title) {
            case "30 crumbs up":
                Debug.Log("U got the 30 crumbs up reward!");
                player.playerStatistic.Crumbs += Value;
                player.crumbAmountText.text = player.playerStatistic.Crumbs.ToString();
                break;
            case "Max health up":
                Debug.Log("U got the max health reward!");
                player.playerStatistic.MaxHealth.AddModifier("maxHealthReward", Value);
                break;
            case "Random upgrade choice":
                Debug.Log("U got the random upgrade choice reward!");
                break;
            default:
                break;
        }
    }
}
