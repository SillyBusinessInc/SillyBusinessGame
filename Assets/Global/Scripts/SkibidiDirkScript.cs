using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class SkibidiDirkScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> apearObjectsThings;
    [SerializeField] private UpgradeOptions upgradeOptions;

    private void Awake()
    {
        apearObjectsThings.ForEach(obj => obj.SetActive(false));
        GlobalReference.SubscribeTo(Events.ALL_ENEMIES_DEAD, OnWaveDone);
        GlobalReference.SubscribeTo(Events.OPEN_UPGRADE_MENU, OnUpgradeMenu);
        GlobalReference.SubscribeTo(Events.GET_EXTRA_HP, ExtraHp);
    }

    private void OnUpgradeMenu()
    {
        upgradeOptions.ShowOptions();
    }

    private void Update()
    {
     
    }

    private void OnWaveDone()
    {
        apearObjectsThings.ForEach(obj => obj.SetActive(true));
    }

    private void ExtraHp()
    {
        var p = GlobalReference.GetReference<PlayerReference>().Player;
        p.playerStatistic.MaxHealth.AddModifier("extra", 2f);
        
        p.playerStatistic.Health += 2f;
        
        if (p.healthBar) p.healthBar.UpdateHealthBar(0f, 
            p.playerStatistic.MaxHealth.GetValue(), p.playerStatistic.Health);
    }
}
