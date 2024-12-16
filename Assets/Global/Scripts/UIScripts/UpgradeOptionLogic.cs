using System;
using TMPro;
using UnityEngine;

public class UpgradeOptionLogic : MonoBehaviour
{
    public UpgradeOption data;

    [HideInInspector] public UnityEngine.UI.Image rarityImage;
    [HideInInspector] public UnityEngine.UI.Image image;
    [HideInInspector] public TMP_Text upgradeName;
    [HideInInspector] public TMP_Text rarity;
    [HideInInspector] public TMP_Text description;
    public RarityColors rarityColors;

    void Start()
    {
        rarityImage = transform.GetChild(1).GetComponent<UnityEngine.UI.Image>();
        image = transform.GetChild(2).GetComponent<UnityEngine.UI.Image>();
        upgradeName = transform.GetChild(3).GetComponent<TMP_Text>();
        rarity = transform.GetChild(4).GetComponent<TMP_Text>();
        description = transform.GetChild(5).GetComponent<TMP_Text>();

        image.sprite = data.image;
        upgradeName.text = data.name;
        description.text = data.description;

        GetRarity(data.rarity);
    }

    private void GetRarity(int rarityId) {
        // use the rarityColors scriptableobject to get the color
        foreach (var rarityColorPair in rarityColors.rarityColorPairs) {
            if (rarityColorPair.rarity == rarityId) {
                rarityImage.sprite = rarityColorPair.rarityBackground;
                rarity.text = rarityColorPair.name;
                rarity.color = rarityColorPair.rarityColor;
                return;
            }
        }
    }
}
