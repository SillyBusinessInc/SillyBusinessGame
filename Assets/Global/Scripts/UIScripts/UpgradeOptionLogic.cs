using System;
using TMPro;
using UnityEngine;

public class UpgradeOptionLogic : MonoBehaviour
{
    public UpgradeOption data;

    [HideInInspector] public UnityEngine.UI.Image rarityImage;
    [HideInInspector] public UnityEngine.UI.Image image;
    [HideInInspector] public TMP_Text upgradeName;
    [HideInInspector] public TMP_Text description;

    public Sprite rarity_common;
    public Sprite rarity_uncommon;
    public Sprite rarity_rare;
    public Sprite rarity_epic;
    public Sprite rarity_legendary;

    void Start()
    {
        rarityImage = transform.GetChild(1).GetComponent<UnityEngine.UI.Image>();
        image = transform.GetChild(2).GetComponent<UnityEngine.UI.Image>();
        upgradeName = transform.GetChild(3).GetComponent<TMP_Text>();
        description = transform.GetChild(4).GetComponent<TMP_Text>();

        SetPanelData(data);
    }

    public void SetPanelData(UpgradeOption upgradeOption)
    {
        data = upgradeOption;

        if (image)
        {
            image.sprite = upgradeOption.image;
        }

        if (upgradeName)
        {
            upgradeName.text = upgradeOption.name;
        }

        if (description)
        {
            description.text = upgradeOption.description;
        }

        if (rarityImage)
        {
            rarityImage.sprite = GetRarityImage(upgradeOption.rarity);
        }
    }

    private Sprite GetRarityImage(int rarity)
    {
        return rarity switch
        {
            1 => rarity_common,
            2 => rarity_uncommon,
            3 => rarity_rare,
            4 => rarity_epic,
            5 => rarity_legendary,
            _ => rarity_common,
        };
    }
}
