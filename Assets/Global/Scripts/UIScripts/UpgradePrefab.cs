using UnityEngine; 
using UnityEngine.UI; 
using TMPro;

public class UpgradePrefab : MonoBehaviour{
    private UnityEngine.UI.Image rarityImage; 
    private UnityEngine.UI.Image upgradeImage; 
    private TMP_Text amountText; 

    public Sprite rarity_common;
    public Sprite rarity_uncommon;
    public Sprite rarity_rare;
    public Sprite rarity_epic;
    public Sprite rarity_legendary;
    public Sprite Starfruit_Platinum;
    public Sprite Ghost_Pepper;
    public Sprite Energy_Drink;
    public Sprite Cant_touch_this;
    public Sprite Leg_Day;
    public Sprite Poppin_it_up;
    public Sprite Popping_Candy;

    private void Start() {
        amountText.text = "0";
        rarityImage = transform.GetChild(1).GetComponent<UnityEngine.UI.Image>();
        upgradeImage = transform.GetChild(2).GetComponent<UnityEngine.UI.Image>();
        amountText = transform.GetChild(3).GetComponent<TMP_Text>();
    }

    void SetData(int rarity, string upgradeOption, int amount = 0) { 
        rarityImage.sprite = GetRarityImage(rarity); 
        upgradeImage.sprite = GetUpgradeImage(upgradeOption); 
        IncreaseAmount();
    } 

    void IncreaseAmount() {
    int currentAmount = int.Parse(amountText.text); 
    currentAmount += 1; 
    amountText.text = currentAmount.ToString();
    }

    Sprite GetRarityImage(int rarity) {
    return rarity switch {
        1 => rarity_common,
        2 => rarity_uncommon,
        3 => rarity_rare,
        4 => rarity_epic,
        5 => rarity_legendary,
        _ => rarity_common,
    };
    }


    Sprite GetUpgradeImage(string upgradeOption) {
    return upgradeOption switch {
        "Starfruit Platinum" => rarity_common,
        "Ghost Pepper" => rarity_uncommon,
        "Energy Drink" => rarity_rare,
        "Can't touch this!" => rarity_epic,
        "Leg Day" => rarity_legendary,
        "Poppin' it up" => rarity_legendary,
        "Popping Candy" => rarity_legendary,
        _ => rarity_common,
    };
    }
}