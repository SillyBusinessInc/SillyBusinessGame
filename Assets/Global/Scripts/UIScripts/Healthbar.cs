// using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private Image healthbarOverlay;
    private Image healthbarUnderlay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Transform healthbarOverlayTransform = transform.GetChild(1);
        Transform healthbarUnderlayTransform = transform.GetChild(0);

        healthbarOverlay = healthbarOverlayTransform.GetComponent<Image>();
        for (int i = 0; i < 2; i++) {
            Instantiate(
                healthbarOverlayTransform.gameObject, 
                new Vector3(healthbarOverlayTransform.position.x - 125, healthbarOverlayTransform.position.y, healthbarOverlayTransform.position.z),
                Quaternion.identity,
                healthbarOverlayTransform.parent
            );
        }

        healthbarUnderlay = healthbarUnderlayTransform.GetComponent<Image>();
        for (int i = 0; i < 2; i++) {
            Instantiate(
                healthbarUnderlayTransform.gameObject, 
                new Vector3(healthbarUnderlayTransform.position.x - 125, healthbarUnderlayTransform.position.y, healthbarUnderlayTransform.position.z),
                Quaternion.identity,
                healthbarUnderlayTransform.parent
            );
        }
    }

    public void UpdateHealthBar(float min, float max, float current)
    {
        float fillAmount = math.lerp(min, max, current / max);
        healthbarOverlay.fillAmount = fillAmount / 100;
        healthbarUnderlay.fillAmount = max / 100;
    }
}
