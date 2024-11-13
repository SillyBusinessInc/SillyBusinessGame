using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private Image healthbarImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthbarImage = this.GetComponent<Image>();
    }

    public void UpdateHealthBar(float min, float max, float current)
    {
        float fillAmount = math.lerp(min, max, current / 100);
        healthbarImage.fillAmount = fillAmount / 100;
    }
}
