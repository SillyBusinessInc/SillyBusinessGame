using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private LayerMask ignoreLayers;
    private Interactable currentInteractable;

    [Header("Raycast Settings [Debug]")]
    [SerializeField] private float rayDistance = 10f;         // Maximum detection distance  
    [SerializeField] private float maxInteractionAngle = 180f; // FOV 


    private bool isColliding = false;

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

        // Check if the player is colliding with an interactable
        bool interactableFound = RaycastFindInteractable(origin, forward);

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
            isColliding = true;
            SetInteractable(interactable);
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

        // First check for overlap sphere, to see if the start of the ray is already colliding with an interactable
        Collider[] colliders = Physics.OverlapSphere(origin, 0.1f, ~ignoreLayers);
        foreach (var collider in colliders)
        {
            var interactable = collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                SetInteractable(interactable);
                return true;
            }
        }

        // Debug visualization
        // Debug.DrawRay(origin, direction * rayDistance, Color.green, 0.1f);

        // Then check for raycast hit
        if (Physics.Raycast(ray, out hit, rayDistance, ~ignoreLayers))
        {
            var interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                float rayHitDistance = Vector3.Distance(transform.position, hit.point);

                if (interactable.IsWithinInteractionRange(rayHitDistance))
                {
                    SetInteractable(interactable);
                    return true;

                }
            }
        }
        return false;
    }

    private void SetInteractable(Interactable interactable)
    {
        if (currentInteractable != interactable)
        {
            if (currentInteractable != null) currentInteractable.ShowPrompt(false);
            currentInteractable = interactable;
            currentInteractable.ShowPrompt(true);
        }
    }
}
