using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private LayerMask interactableLayer;
    private Interactable currentInteractable;
    private Player player;

    [SerializeField] private float rayRadius = 1.5f;         // Radius for SphereCast
    [SerializeField] private float rayDistance = 4f;         // Maximum detection distance
    [SerializeField] private float maxInteractionAngle = 44f; // Desired field of view (half-angle)
    private void Start()
    {
        player = GlobalReference.GetReference<PlayerReference>().Player;
    }

    private void Update()
    {
        DetectInteractable();

        // Trigger interaction if a valid interactable is within range 
        if (currentInteractable != null && player.inputActions.actions["Interact"].triggered)
        {
            TriggerInteraction();
        }
    }

    private void DetectInteractable()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Use SphereCast to check for interactables within the radius
        if (Physics.SphereCast(ray, rayRadius, out hit, rayDistance, interactableLayer))
        {
            var interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null && interactable.IsWithinInteractionRange(transform.position) && !interactable.isDisabled)
            {
                // Calculate the direction to the interactable and the angle between the player's forward direction
                Vector3 directionToInteractable = (interactable.transform.position - transform.position).normalized;
                float angleToInteractable = Vector3.Angle(transform.forward, directionToInteractable);

                // Check if the interactable is within the desired angle (45 degrees)
                if (angleToInteractable <= maxInteractionAngle)
                {
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

        // If no interactable is found, clear the current interactable
        if (currentInteractable != null)
        {
            currentInteractable.ShowPrompt(false);
            currentInteractable = null;
        }
    }


    private void TriggerInteraction()
    {
        if (currentInteractable != null)
        {
            currentInteractable.OnInteract();
        }
    }
}
