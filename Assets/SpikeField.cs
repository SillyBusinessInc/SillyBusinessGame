using UnityEngine;
using System.Collections;

public class SpikeField : MonoBehaviour
{
    [SerializeField] private int damage = 10; // Damage dealt by the cone
    [SerializeField] private float disableDuration = 0.1f; // Avoid damage loop
    [SerializeField] private float knockbackForce = 10f; // Horizontal knockback speed
    [SerializeField] private float leapForce = 5f; // Vertical leap speed
    private PlayerReference player;

    private bool isDisabled = false;

    private void Start()
    {
        // Ensure Player reference is set correctly
        player = GlobalReference.GetReference<PlayerReference>();

        if (player == null)
        {
            Debug.LogError("Player reference not found.");
        }
    }

    private void OnCollisionEnter(Collision entity)
    {

        if (entity.gameObject.CompareTag("Player"))
        {
            if (player != null)
            {
                if (!isDisabled) player.Player.OnHit(damage);

                // Calculate and apply knockback velocity
                Vector3 knockbackVelocity = CalculateKnockback(player.PlayerObj);
                player.Player.ApplyKnockback(knockbackVelocity, 3);
            }

            isDisabled = true;
            StartCoroutine(ReEnableAfterDelay());
        }
    }

    public virtual Vector3 CalculateKnockback(PlayerObject playerObject)
    {
        Vector3 directionToPlayer = playerObject.transform.position - transform.position;
        directionToPlayer.Normalize();

        Vector3 knockbackVelocity = directionToPlayer * knockbackForce;
        knockbackVelocity.y = leapForce; // Add upward velocity for a leap

        return knockbackVelocity * 3;
    }

    // Coroutine to re-enable the spike cone after a delay
    private IEnumerator ReEnableAfterDelay()
    {
        yield return new WaitForSeconds(disableDuration);
        isDisabled = false;
    }
}
