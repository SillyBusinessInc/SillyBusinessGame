/////////////////////////////////////////////////////////////////////////////
///                             OBSOLETE                                  ///
/////////////////////////////////////////////////////////////////////////////

using System.Collections;
using UnityEngine;

// Base enemy class
public abstract class EnemyBase : MonoBehaviour
{
    [Header("Base Enemy Fields")]
    [SerializeField]
    [Range(0, 250)]
    protected int health = 100;
    [SerializeField] private float invulnerabilityTime = 0.75f;
    private bool isInvulnerable = false;

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
        if (isInvulnerable) return;

        health -= damage;
        if (health <= 0)
        {
            OnDeath();
        }

        isInvulnerable = true;

        // Start invulnerability timer
        StartCoroutine(InvulnerabilityTimer());

    }

    private IEnumerator InvulnerabilityTimer()
    {
        yield return new WaitForSeconds(invulnerabilityTime);
        isInvulnerable = false;
    }


    virtual public void OnDeath()
    {

        GlobalReference.AttemptInvoke(Events.ENEMY_KILLED);
        Destroy(gameObject);
    }
    void OnDestroy()
    {
        OnDeath();
    }
}
