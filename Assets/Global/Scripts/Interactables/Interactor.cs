using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private LayerMask ignoreLayers;
    private Interactable currentInteractable;
    private Player player;

    [Header("Raycast Settings [Debug]")]
    [SerializeField] private float rayDistance = 10f;         // Maximum detection distance 
    [SerializeField] private float rayOffset = 0.5f;         // offset

    private bool isColliding = false;

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
        if (currentInteractable != null && !currentInteractable.IsDisabled && ctx.started)
        {
            currentInteractable.TriggerInteraction();
        }
    }

    private void DetectInteractable()
    {
        // move backwards from the player's position
        Vector3 origin = transform.position + Vector3.up * 0.1f + transform.forward * -1.0f;
        Vector3 forward = transform.forward;

        Vector3[] offsets =
        {
            Vector3.zero,                        // center
            transform.right * rayOffset,         // right
            -transform.right * rayOffset,        // left
            transform.up * rayOffset,            // up
            -transform.up * rayOffset            // down
        };

        bool interactableFound = false;

        foreach (Vector3 offset in offsets)
        {
            if (RaycastFindInteractable(origin + offset, forward))
            {
                interactableFound = true;
                return;
            }
        }

        // If no interactable was found, clear the current interactable
        if (!interactableFound && currentInteractable != null && !isColliding)
        {
            Debug.Log("Clearing current interactable");
            currentInteractable.ShowPrompt(false);
            currentInteractable = null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

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

    private bool RaycastFindInteractable(Vector3 origin, Vector3 direction)
    {
        Ray ray = new Ray(origin, direction);
        RaycastHit hit;

        // Debug visualization
        Debug.DrawRay(origin, direction * rayDistance, Color.green, 0.1f);

        if (Physics.Raycast(ray, out hit, rayDistance, ~ignoreLayers))
        {
            var interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                float rayHitDistance = Vector3.Distance(transform.position, hit.point);

                if (interactable.IsWithinInteractionRange(rayHitDistance))
                {
                    if (currentInteractable != interactable)
                    {
                        if (currentInteractable != null) currentInteractable.ShowPrompt(false);
                        currentInteractable = interactable;
                        currentInteractable.ShowPrompt(true);
                    }
                    return true; // Interactable found

                }
            }
        }
        return false; // No valid interactable detected
    }
}
