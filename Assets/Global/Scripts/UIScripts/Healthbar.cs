using UnityEngine;

public class Healthbar : MonoBehaviour
{
    private RectTransform HealthbarUnderlayTransform;
    private RectTransform HealthbarOverlayTransform;
    private float originalWidth;
    private Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        HealthbarUnderlayTransform = transform.GetChild(0) as RectTransform;
        HealthbarOverlayTransform = transform.GetChild(1) as RectTransform;

        originalWidth = HealthbarUnderlayTransform.sizeDelta.x;
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
        HealthbarOverlayTransform.sizeDelta = new Vector2(
            originalWidth * (currentHealth / 2), 
            HealthbarOverlayTransform.sizeDelta.y
        );
    }

    public void UpdateMaxHealth() {
        // doing maxHealth / 2 because 1hp is a half heart
        float maxHealth = player.playerStatistic.MaxHealth.GetValue();
        HealthbarUnderlayTransform.sizeDelta = new Vector2(
            originalWidth * (maxHealth / 2), 
            HealthbarUnderlayTransform.sizeDelta.y
        );
    }
}
