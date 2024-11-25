using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private Transform HealthbarUnderlayTransform;
    private Transform HealthbarOverlayTransform;
    private float originalWidth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        HealthbarUnderlayTransform = transform.GetChild(0);
        HealthbarOverlayTransform = transform.GetChild(1);

        originalWidth = (HealthbarUnderlayTransform as RectTransform).sizeDelta.x;

        UpdateMaxHealth();
    }

    public void UpdateHealthBar(float current)
    {
        (HealthbarOverlayTransform as RectTransform).sizeDelta = new Vector2(
            originalWidth * (current / 2), 
            (HealthbarOverlayTransform as RectTransform).sizeDelta.y
        );
    }

    public void UpdateMaxHealth() {
        Player player = GlobalReference.GetReference<PlayerReference>().Player;

        // doing maxHealth / 2 because 1hp is a half heart
        (HealthbarUnderlayTransform as RectTransform).sizeDelta = new Vector2(
            originalWidth * (player.playerStatistic.MaxHealth.GetValue() / 2), 
            (HealthbarUnderlayTransform as RectTransform).sizeDelta.y
        );

        (HealthbarOverlayTransform as RectTransform).sizeDelta = new Vector2(
            originalWidth * (player.playerStatistic.MaxHealth.GetValue() / 2), 
            (HealthbarOverlayTransform as RectTransform).sizeDelta.y
        );
    }
}
