using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class UpgradeList : MonoBehaviour
{
    public UpgradePrefab upgradePrefab;
    public Transform contentParent;
    public float itemSpacing = 10f;
    public int itemsPerRow = 3;
    private List<UpgradePrefab> upgrades = new List<UpgradePrefab>();

    public void AddUpgrade(int rarity, Sprite upgradeOption)
    {
        foreach (var upgrade in upgrades)
        {
            var data = upgrade.GetData();
            if (data.Item1 == rarity && data.Item2 == upgradeOption)
            {
                upgrade.IncreaseAmount();
                return;
            }
        }

        UpgradePrefab newUpgrade = Instantiate(upgradePrefab, contentParent);
        newUpgrade.SetData(rarity, upgradeOption);

        upgrades.Add(newUpgrade);
        RectTransform rectTransform = newUpgrade.GetComponent<RectTransform>();

        int index = upgrades.Count - 1;
        int row = index / itemsPerRow;
        int col = index % itemsPerRow;

        float xPos = col * (rectTransform.rect.width + itemSpacing);
        float yPos = -row * (rectTransform.rect.height + itemSpacing);

        rectTransform.anchoredPosition = new Vector2(xPos, yPos);
    }
}
