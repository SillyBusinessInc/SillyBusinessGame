using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoldMeter : MonoBehaviour
{
    private Player player;
    [SerializeField] private TMP_Text MoldPercentageText;
    [SerializeField] private float moldPercentage;
    [SerializeField] private RectTransform MoldMeterImage;
    private Vector2 PositionMold;
    private float OriginalPosX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        PositionMold = MoldMeterImage.anchoredPosition;
        OriginalPosX = PositionMold.x;
    }

    void Start()
    {
        player = GlobalReference.GetReference<PlayerReference>().Player;

        UpdateMoldMeter();
        GlobalReference.SubscribeTo(Events.MOLDMETER_CHANGED, UpdateMoldMeter);
    }

    public void UpdateMoldMeter()
    {
        moldPercentage = player.playerStatistic.Moldmeter;
        string decimals = moldPercentage >= 100 || moldPercentage == 0 ? "F0" : "F1";
        MoldPercentageText.text = moldPercentage.ToString(decimals) + '%';

        // when moldmeter is 0%, posX of mold should be -88. so it should move 2.71 per 1%
        // when moldmeter is 100% the posX is 183
        PositionMold.x = 2.71f * moldPercentage + OriginalPosX; // move the moldImage to the right based on the percentage
        MoldMeterImage.anchoredPosition = PositionMold;
    }
}
