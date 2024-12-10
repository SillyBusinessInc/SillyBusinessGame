using UnityEngine;
using UnityEngine.UI;
public class EnemyHealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public float maxHealth = 100f;
    public float currentHealth;
    public EnemiesNS.EnemyBase enemyBase;

    void Start()
    {
        healthSlider.maxValue = enemyBase.health;
    }
    void Update()
    {
        healthSlider.value = enemyBase.health;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }
}
