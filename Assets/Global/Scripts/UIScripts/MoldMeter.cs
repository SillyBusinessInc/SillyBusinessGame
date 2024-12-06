using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoldMeter : MonoBehaviour
{
    private Player player;
    [SerializeField] private TMP_Text MoldPercentageText;
    [SerializeField] private float moldPercentage;


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        player = GlobalReference.GetReference<PlayerReference>().Player;

        UpdateMoldMeter();
        GlobalReference.SubscribeTo(Events.MOLDMETER_CHANGED, UpdateMoldMeter);
    }

    [ContextMenu("UpdateMoldMeter")]
    public void UpdateMoldMeter()
    {
        moldPercentage = player.playerStatistic.Moldmeter;
        string decimals = moldPercentage >= 100 ? "F0" : "F1";
        MoldPercentageText.text = moldPercentage.ToString(decimals);
    }
}
