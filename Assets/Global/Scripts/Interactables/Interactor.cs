using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private LayerMask interactableLayer;
    private Interactable currentInteractable;
    private Player player;

    [SerializeField] private float rayRadius = 1.0f;       // Radius of the sphere cast
    [Range(0, 10)]
    [SerializeField] private float rayDistance = 5f;         // Maximum detection distance
    [SerializeField] private float maxInteractionAngle = 180f; // Desired field of view  
    private void Start()
    {
        player = GlobalReference.GetReference<PlayerReference>().Player;
    }

    private void Update()
    {
        DetectInteractable();
    }

    public void Interact(InputAction.CallbackContext ctx)
    {
        if (currentInteractable != null && ctx.started)
        {
            currentInteractable.OnInteract();
        }
    }

    private void DetectInteractable()
    {
        Vector3 offset = transform.forward * 0.1f + Vector3.up * 0.1f; // Slight offset
        Ray ray = new Ray(transform.position + offset, transform.forward);
        RaycastHit hit;

        // Check for nearby objects
        if (Physics.SphereCast(ray, rayRadius, out hit, rayDistance, interactableLayer))
        {
            var interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null && interactable.IsWithinInteractionRange(transform.position))
            {
                // Calculate the direction to the interactable and the angle between the player's forward direction
                Vector3 directionToInteractable = (interactable.transform.position - transform.position).normalized;
                float angleToInteractable = Vector3.Angle(transform.forward, directionToInteractable);

                // Check if the interactable is within the desired angle
                if (angleToInteractable <= maxInteractionAngle)
                {
                    // If we find a new interactable, update it
                    if (currentInteractable != interactable)
                    {
                        if (currentInteractable != null) currentInteractable.ShowPrompt(false);
                        currentInteractable = interactable;
                        currentInteractable.ShowPrompt(true);
                    }
                    return;
                }
            }
        }

        // If no valid interactable is found, clear the current interactable
        if (currentInteractable != null)
        {
            currentInteractable.ShowPrompt(false);
            currentInteractable = null;
        }
    }

}
