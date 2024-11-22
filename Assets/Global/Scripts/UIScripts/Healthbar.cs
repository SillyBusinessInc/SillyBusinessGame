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
        healthbarOverlay = transform.GetChild(1).GetComponent<Image>();
        healthbarUnderlay = transform.GetChild(0).GetComponent<Image>();
    }

    public void UpdateHealthBar(float min, float max, float current)
    {
        float fillAmount = math.lerp(min, max, current / max);
        healthbarOverlay.fillAmount = fillAmount / 100;
        healthbarUnderlay.fillAmount = max / 100;
    }
}
