using UnityEngine;

// Base enemy class
public abstract class EnemyBase : MonoBehaviour
{
    [Header("Base Enemy Fields")]
    [SerializeField]
    [Range(0, 250)]
    private int health = 100;

    protected void Start()
    {
        GlobalReference.AttemptInvoke(Events.ENEMY_SPAWNED);
    }

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

    public void OnHit(int damage)
    {
        health -= damage;
        Debug.Log(health);
        if (health <= 0)
        {
            OnDeath();
        }
    }

    void OnDeath()
    {
        GlobalReference.AttemptInvoke(Events.ENEMY_KILLED);
        Destroy(gameObject);
    }
}
