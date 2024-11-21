using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{

    [Header("Interaction Settings")]
    [SerializeField] private string interactionPrompt = "E - Interact";
    [SerializeField] private string disabledPrompt = "Cannot interact";

    [SerializeField]
    [Range(-10f, 10f)]
    private float promptYOffset = 1.5f;

    [Range(0, 10)]
    [SerializeField] private float interactDistance = 5.0f;


    [Header("Initial State")]
    [SerializeField]
    private bool isDisabled = false;
    private Camera playerCamera;

    // list of scriptable objects that will be invoked when the interactable is triggered 
    [Header("Actions")]
    [SerializeField] private UnityEvent interactionActions;
    [SerializeField] private UnityAction failedInteractionActions;
    public bool IsDisabled
    {
        get => isDisabled;
        set
        {
            isDisabled = value;
            SetBillboardText();

            if (isDisabled)
            {
                OnDisableInteraction();
            }

            if (!isDisabled)
            {
                OnEnableInteraction();
            }
        }
    }

    private GameObject hudElement;

    protected void Start()
    {
        playerCamera = GlobalReference.GetReference<PlayerReference>().PlayerCamera;

        // Create a HUD element to display the interaction prompt
        if (hudElement == null)
        {
            InstantiateHUD();
        }

        IsDisabled = isDisabled;
    }

    private void InstantiateHUD()
    {
        // Create a HUD element to display the interaction prompt
        hudElement = new GameObject("HUDPrompt");
        hudElement.AddComponent<TextMesh>().text = interactionPrompt;
        hudElement.transform.localScale = Vector3.one * 0.2f;
        hudElement.SetActive(false);
        hudElement.GetComponent<TextMesh>().anchor = TextAnchor.MiddleCenter;


        // set right coordinates
        hudElement.transform.SetParent(transform);

    }

    public bool IsWithinInteractionRange(float rayHitDistance)
    {
        return rayHitDistance <= interactDistance;
    }

    public void ShowPrompt(bool show)
    {
        hudElement.SetActive(show);
        if (show)
        {
            hudElement.transform.position = transform.position + Vector3.up * promptYOffset;
            RotateBillboardTowardsCamera();
            SetBillboardText();
        }
    }

    private void Update()
    {
        if (hudElement != null && playerCamera != null) RotateBillboardTowardsCamera();
    }

    private void RotateBillboardTowardsCamera()
    {
        Vector3 directionToCamera = playerCamera.transform.position - hudElement.transform.position;
        hudElement.transform.rotation = Quaternion.LookRotation(-directionToCamera);
    }

    public virtual void OnInteract() { }

    public virtual void OnFailedInteract() { }

    public virtual void OnEnableInteraction() { }

    public virtual void OnDisableInteraction() { }

    public void TriggerInteraction()
    {
        if (!isDisabled)
        {
            OnInteract();
            // Invoke all actions
            interactionActions.Invoke();
        }
        else
        {
            OnFailedInteract();
            failedInteractionActions.Invoke();
        }
    }


    private void SetBillboardText()
    {
        // if no HUD element, send log message
        if (hudElement == null)
        {
            Debug.Log("[Improper Configuration] No HUD element found, make sure the inherited class calls the base.Start() method [Interactable.cs]");
            InstantiateHUD();
        }

        hudElement.GetComponent<TextMesh>().text = isDisabled ? disabledPrompt : interactionPrompt;

        if (string.IsNullOrEmpty(hudElement.GetComponent<TextMesh>().text))
        {
            hudElement.SetActive(false);
        }
    }
}
