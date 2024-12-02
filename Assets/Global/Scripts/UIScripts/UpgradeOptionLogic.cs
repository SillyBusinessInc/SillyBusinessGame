using System;
using TMPro;
using UnityEngine;

public class UpgradeOptionLogic : MonoBehaviour
{
    public UpgradeOption data;

    [NonSerialized] public UnityEngine.UI.Image image;
    [NonSerialized] public TMP_Text upgradeName;
    [NonSerialized] public TMP_Text description;

    

    void Start()
    {
        image = transform.GetChild(1).GetComponent<UnityEngine.UI.Image>();
        upgradeName = transform.GetChild(2).GetComponent<TMP_Text>();
        description = transform.GetChild(3).GetComponent<TMP_Text>();

        image.sprite = data.image;
        upgradeName.text = data.name;
        description.text = data.description;
    }
}
