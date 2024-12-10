using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealthBar : MonoBehaviour
{
    public Slider healthSlider;
    private float maxHealth = 100f;
    private float currentHealth;
    public EnemiesNS.EnemyBase enemyBase;

    void Start()
    {
        healthSlider.maxValue = enemyBase.health;
    }
    void Update()
    {
        if (enemyBase.HealthBarDetroy)
        {
            Destroy(gameObject);
        }
        healthSlider.value = enemyBase.health;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }
}
