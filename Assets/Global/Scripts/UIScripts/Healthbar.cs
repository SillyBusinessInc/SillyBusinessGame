using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private RectTransform UnderlayHalfHealth;
    private RectTransform OverlayHalfHealth;
    private float originalWidth;
    private Player player;
    private RectTransform UnderlayWholeHealth;
    private RectTransform OverlayWholeHealth;
    private bool isHalf;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // getting the half hearts
        UnderlayHalfHealth = transform.GetChild(0) as RectTransform;
        OverlayHalfHealth = transform.GetChild(1) as RectTransform;

        // getting the whole hearts, which is a child object of the halfheart
        UnderlayWholeHealth = UnderlayHalfHealth.GetChild(0) as RectTransform;
        OverlayWholeHealth = OverlayHalfHealth.GetChild(0) as RectTransform;

        originalWidth = UnderlayHalfHealth.sizeDelta.x;
    }

    void Start()
    {
        player = GlobalReference.GetReference<PlayerReference>().Player;

        UpdateHealthBar();
        GlobalReference.SubscribeTo(Events.HEALTH_CHANGED, UpdateCurrentHealth);
        player.playerStatistic.MaxHealth.Subscribe(UpdateMaxHealth);
    }

    public void UpdateHealthBar()
    {
        UpdateMaxHealth();
        UpdateCurrentHealth();
    }
    
    public void UpdateCurrentHealth()
    {
        float currentHealth = player.playerStatistic.Health;
        isHalf = currentHealth % 2 != 0;
        // if a half heart needs to be displayed, display the half heart image as the last image
        if (isHalf) {
            OverlayWholeHealth.sizeDelta = new Vector2(
                originalWidth * ((currentHealth - 1) / 2),
                OverlayWholeHealth.sizeDelta.y
            );
            // make sure to see the the half heart because the width of whole heart is smaller than the width of half heart
            OverlayHalfHealth.sizeDelta = new Vector2(
                originalWidth * ((currentHealth + 1) / 2),
                OverlayHalfHealth.sizeDelta.y
            );
        } else {
            OverlayWholeHealth.sizeDelta = new Vector2(
                originalWidth * (currentHealth / 2), 
                OverlayWholeHealth.sizeDelta.y
            );

            // putting the width to 0 to make sure we only see the whole hearts
            OverlayHalfHealth.sizeDelta = new Vector2(
                0,
                OverlayHalfHealth.sizeDelta.y
            );
        }
    }
    public void UpdateMaxHealth() {
        // doing maxHealth / 2 because 1hp is a half heart
        float maxHealth = player.playerStatistic.MaxHealth.GetValue();
        isHalf = maxHealth % 2 != 0;
        if (isHalf) {
            UnderlayWholeHealth.sizeDelta = new Vector2(
                originalWidth * ((maxHealth - 1) / 2), 
                UnderlayWholeHealth.sizeDelta.y
            );

            UnderlayHalfHealth.sizeDelta = new Vector2(
                originalWidth * ((maxHealth + 1) / 2), 
                UnderlayHalfHealth.sizeDelta.y
            );
        } else {
            UnderlayWholeHealth.sizeDelta = new Vector2(
                originalWidth * (maxHealth / 2), 
                UnderlayWholeHealth.sizeDelta.y
            );

            UnderlayHalfHealth.sizeDelta = new Vector2(
                0,
                UnderlayHalfHealth.sizeDelta.y
            );
        }
    }
}
