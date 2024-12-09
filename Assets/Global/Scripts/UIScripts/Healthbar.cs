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
            ChangeHealthWidth(OverlayWholeHealth, currentHealth - 1);
            ChangeHealthWidth(OverlayHalfHealth, currentHealth + 1);
        } else {
            ChangeHealthWidth(OverlayWholeHealth, currentHealth);
            ChangeHealthWidth(OverlayHalfHealth, 0);
        }
    }
    public void UpdateMaxHealth() {
        // doing maxHealth / 2 because 1hp is a half heart
        float maxHealth = player.playerStatistic.MaxHealth.GetValue();
        isHalf = maxHealth % 2 != 0;
        if (isHalf) {
            ChangeHealthWidth(UnderlayWholeHealth, maxHealth - 1);
            ChangeHealthWidth(UnderlayHalfHealth, maxHealth + 1);
        } else {
            ChangeHealthWidth(UnderlayWholeHealth, maxHealth);
            ChangeHealthWidth(UnderlayHalfHealth, 0);
        }
    }

    private void ChangeHealthWidth(RectTransform healthbar, float newHealth) {
        healthbar.sizeDelta = new Vector2(
            originalWidth * newHealth / 2, 
            healthbar.sizeDelta.y
        );
    }
}