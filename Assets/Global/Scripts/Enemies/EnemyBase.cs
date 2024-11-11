using UnityEngine;

// Base enemy class
public abstract class EnemyBase : MonoBehaviour
{
    [Header("Base Enemy Fields")]
    [SerializeField]
    [Range(0, 250)]
    public float health = 100; // private -> public, int -> float

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

    public void OnHit(float damage) // int -> float
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
        Destroy(gameObject); // add
    }
}
