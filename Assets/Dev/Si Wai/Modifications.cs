using UnityEngine;
using TMPro;

public class Modifications : MonoBehaviour
{
    public TMP_Text speedText;
    public TMP_Text jumpForceText;
    public TMP_Text crumbAmountText;
    public TMP_Text maxHealthText;
    public TMP_Text AttackSpeedMultiplier;
    public TMP_Text AttackDamageMultiplierText;
    public TMP_Text DodgeCooldownText;
    public TMP_Text DoubleJumpsCount;

    void Start() 
    {
        crumbAmountText.text = GlobalReference.PermanentPlayerStatistic.Get<float>("crumbs").ToString();
    //     speedText.text = GlobalReference.PlayerStatisticPermanent.Speed.GetValue().ToString();
    //     maxHealthText.text = GlobalReference.PlayerStatisticPermanent.MaxHealth.GetValue().ToString();

    //     var f = GlobalReference.PlayerStatisticPermanent.Get<string>("maxHealth");
    //     foreach (var kvp in GlobalReference.JsonToList(f)) {
    //         Debug.Log($"Key: {kvp.Key}, Value: {kvp.Value}");
    //     }

    //     GlobalReference.PlayerStatisticPermanent.MaxHealth.Subscribe(ChangeMaxHealth);
        GlobalReference.SubscribeTo(Events.CRUMBS_CHANGED, ChangeMaxHealth);
    
    }

    void ChangeMaxHealth() {
        Debug.Log("f");
        crumbAmountText.text = GlobalReference.PermanentPlayerStatistic.Crumbs.ToString();

    //     var f = GlobalReference.PlayerStatisticPermanent.Get<string>("maxHealth");
    //     Debug.Log(f);
    //     foreach (var kvp in GlobalReference.JsonToList("maxHealth")) {
    //         Debug.Log($"Key: {kvp.Key}, Value: {kvp.Value}");
    //     }
    }
}