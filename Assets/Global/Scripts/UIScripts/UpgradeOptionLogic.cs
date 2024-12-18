using System;
using TMPro;
using UnityEngine;

public class UpgradeOptionLogic : MonoBehaviour
{
    public UpgradeOption data;
    public UnityEngine.UI.Image image;
    [HideInInspector] public TMP_Text upgradeName;
    [HideInInspector] public TMP_Text description;

    void Start()
    {
        upgradeName = transform.GetChild(0).GetComponent<TMP_Text>();
        description = transform.GetChild(1).GetComponent<TMP_Text>();

        image.sprite = data.image;
        upgradeName.text = data.name;
        description.text = data.description ?? "Hmm yes, yeast of power. So powerful";
    }

    /*private void GetRarity(int rarityId) {
        // use the rarityColors scriptableobject to get the color
        foreach (var rarityColorPair in rarityColors.rarityColorPairs) {
            if (rarityColorPair.rarity == rarityId) {
                rarityImage.sprite = rarityColorPair.rarityBackground;
                rarity.text = rarityColorPair.name;
                rarity.color = rarityColorPair.rarityColor;
                return;
            }
        }
    }*/
}
