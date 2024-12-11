using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpikeField : MonoBehaviour
{
    [SerializeField] private int damage = 10; // Damage dealt by the cone
    [SerializeField] private int enemyDamage = 10; // Damage dealt by the cone
    [SerializeField] private float disableDuration = 0.1f; // Avoid damage loop
    [SerializeField] private float knockbackForce = 10f; // Horizontal knockback speed
    [SerializeField] private float leapForce = 5f; // Vertical leap speed
    private Player player;

    private List<GameObject> hitEntities = new();
    private Dictionary<string, System.Action<GameObject>> tagHandlers;

    private bool isDisabled = false;

    private void Start()
    {
        // Ensure Player reference is set correctly
        player = GlobalReference.GetReference<PlayerReference>().Player;

        if (player == null)
        {
            Debug.LogError("Player reference not found.");
        }

        // Initialize tag handlers
        tagHandlers = new()
        {
            { "Player", HandlePlayer },
            { "Enemy", HandleEnemy }
        };
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject entity = collision.gameObject;
        if (tagHandlers.TryGetValue(entity.tag, out var handler))
        {
            handler(entity);
        }
    }

    private void HandlePlayer(GameObject entity)
    {

        if (player == null) return;

        PlayerObject playerObj = GlobalReference.GetReference<PlayerReference>().PlayerObj;

        if (AddToHitEntities(entity))
        {
            player.OnHit(damage);

            Vector3 knockback = ApplyKnockbackToGO.CalculateKnockback(gameObject, playerObj.gameObject, knockbackForce, leapForce);

            ApplyKnockbackToGO.ApplyKnockback(playerObj.gameObject, knockback, disableDuration, this);

        }
    }

    private void HandleEnemy(GameObject entity)
    {
        EnemyBase enemy = entity.GetComponent<EnemyBase>();
        if (enemy == null) return;

        if (!isDisabled) enemy.OnHit(enemyDamage);

        Vector3 knockback = ApplyKnockbackToGO.CalculateKnockback(gameObject, enemy.gameObject, knockbackForce, leapForce);
        ApplyKnockbackToGO.ApplyKnockback(enemy.gameObject, knockback, disableDuration, this);
    }

    private bool AddToHitEntities(GameObject entity)
    {
        hitEntities.Add(entity);
        StartCoroutine(ReEnableAfterDelay(entity));

        return true;
    }


    // Coroutine to re-enable the spike cone after a delay
    private IEnumerator ReEnableAfterDelay(GameObject entity)
    {
        yield return new WaitForSeconds(disableDuration);
        hitEntities.Remove(entity);
    }
}
