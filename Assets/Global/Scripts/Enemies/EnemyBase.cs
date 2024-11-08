using UnityEngine;

// Base enemy class
public abstract class EnemyBase : MonoBehaviour
{
    // Base enemy fields
    [SerializeField]
    [Range(0, 250)]
    private int health = 100;

    void Update()
    {
        // Base enemy update
    }

    void Move()
    {
        // Base enemy Move-function
    }

    void Attack()
    {
        // Base enemy Attack-function
    }

    void OnHit(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            OnDeath();
        }
    }

    void OnDeath()
    {
        // Base enemy OnDeath-function
        Debug.Log($"{this.name} OnDeath() triggered", this);
    }
}
