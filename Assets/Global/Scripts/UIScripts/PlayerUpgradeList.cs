using UnityEngine;
using System.Collections.Generic;

public class UpgradeList : MonoBehaviour { 

    public GameObject upgradePrefab; 
    public Transform contentParent; 
    public float itemSpacing = 10f;
    private List<GameObject> upgrades = new List<GameObject>(); 

    public void AddUpgrade() { 
        GameObject newUpgrade = Instantiate(upgradePrefab, contentParent); upgrades.Add(newUpgrade); 
        RectTransform rectTransform = newUpgrade.GetComponent<RectTransform>(); 
        
        int index = upgrades.Count - 1; 
        float xPos = index * (rectTransform.rect.width + itemSpacing); 
        rectTransform.anchoredPosition = new Vector2(xPos, 0); 
    }

}