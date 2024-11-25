using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private RectTransform HealthbarUnderlayTransform;
    private RectTransform HealthbarOverlayTransform;
    private float originalWidth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        HealthbarUnderlayTransform = transform.GetChild(0) as RectTransform;
        HealthbarOverlayTransform = transform.GetChild(1) as RectTransform;

        originalWidth = HealthbarUnderlayTransform.sizeDelta.x;

        UpdateMaxHealth();
    }

    public void UpdateHealthBar(float current)
    {
        HealthbarOverlayTransform.sizeDelta = new Vector2(
            originalWidth * (current / 2), 
            HealthbarOverlayTransform.sizeDelta.y
        );
    }

    public void UpdateMaxHealth() {
        Player player = GlobalReference.GetReference<PlayerReference>().Player;

        // doing maxHealth / 2 because 1hp is a half heart
        HealthbarUnderlayTransform.sizeDelta = new Vector2(
            originalWidth * (player.playerStatistic.MaxHealth.GetValue() / 2), 
            HealthbarUnderlayTransform.sizeDelta.y
        );

        HealthbarOverlayTransform.sizeDelta = new Vector2(
            originalWidth * (player.playerStatistic.MaxHealth.GetValue() / 2), 
            HealthbarOverlayTransform.sizeDelta.y
        );
    }
}
