using UnityEngine;
using UnityEngine.UI;
public class EnemyHealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public EnemiesNS.EnemyBase enemyBase;

    void Start()
    {
        healthSlider.maxValue = enemyBase.maxHealth;
    }
    void Update()
    {
        if (enemyBase.HealthBarDestroy)
        {
            Destroy(gameObject);
        }
        healthSlider.value = enemyBase.health;
    }
}
