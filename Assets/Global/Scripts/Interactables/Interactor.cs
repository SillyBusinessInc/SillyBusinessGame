using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private LayerMask ignoreLayers;
    private Interactable currentInteractable;
    private Player player;

    [SerializeField] private float rayRadius = 1.0f;       // Radius of the sphere cast
    [Range(0, 10)]
    [SerializeField] private float rayDistance = 5f;         // Maximum detection distance
    [SerializeField] private float maxInteractionAngle = 180f; // Desired field of view 

    private bool isColliding = false;

    private void Start()
    {
        player = GlobalReference.GetReference<PlayerReference>().Player;
    }

    private void Update()
    {
        DetectInteractable();
    }

    private void OnCollision(Collision collision)
    {
        if (isColliding) return;


        var interactable = collision.collider.GetComponent<Interactable>();
        if (interactable != null)
        {

            Debug.Log("Collision detected", gameObject);
            isColliding = true;
            currentInteractable = interactable;
            currentInteractable.ShowPrompt(true);
        }
        else
        {
            isColliding = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isColliding = false;
        var interactable = collision.collider.GetComponent<Interactable>();
        if (interactable != null && interactable == currentInteractable)
        {
            currentInteractable.ShowPrompt(false);
            currentInteractable = null;
        }
    }

    public void Interact(InputAction.CallbackContext ctx)
    {
        if (currentInteractable != null && !currentInteractable.IsDisabled && ctx.started)
        {
            currentInteractable.TriggerInteraction();
        }
    }

    private void DetectInteractable()
    {
        Vector3 offset = transform.forward * -1.3f + Vector3.up * 0.1f; // Slight offset
        Ray ray = new Ray(transform.position + offset, transform.forward);
        RaycastHit hit;

        Debug.DrawLine(ray.origin, ray.origin + ray.direction * rayDistance, Color.green, 2f);

        // Draw the sphere at the end point
        Debug.DrawRay(ray.origin + ray.direction * rayDistance, Vector3.up * rayRadius, Color.green, 2f);
        // Check for nearby objects, prioritizing interactables that are in front of the player
        if (Physics.SphereCast(ray, rayRadius, out hit, rayDistance, ~ignoreLayers))
        {
            var interactable = hit.collider.GetComponent<Interactable>();

            float rayHitDistance = Vector3.Distance(transform.position, hit.point);

            if (interactable != null && interactable.IsWithinInteractionRange(rayHitDistance))
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
        if (currentInteractable != null && !isColliding)
        {
            Debug.Log("Clearing current interactable");
            currentInteractable.ShowPrompt(false);
            currentInteractable = null;
        }
    }

    private void SetInteractableFromHitIfValid(RaycastHit hit)
    {

    }
}
