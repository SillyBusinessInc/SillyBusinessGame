using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    // Called when the player collects this item
    public abstract void OnCollect();

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            OnCollect(); // Trigger specific behavior
            Destroy(gameObject); // Destroy the collectable
        }
    }
}
