using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{

    [SerializeField] private float interactDistance = 2.0f;
    [SerializeField] private string interactionPrompt = "E - Interact";
    [SerializeField] private string disabledPrompt = "Cannot interact";
    private Camera playerCamera;

    [SerializeField]
    [Range(0, 10f)]
    private float promptYOffset = 1.5f;

    public bool isDisabled = false;

    private GameObject hudElement;

    private void Start()
    {
        playerCamera = GlobalReference.GetReference<PlayerReference>().PlayerCamera;

        // Create a HUD element to display the interaction prompt
        hudElement = new GameObject("HUDPrompt");
        hudElement.AddComponent<TextMesh>().text = interactionPrompt;
        hudElement.transform.localScale = Vector3.one * 0.2f;
        hudElement.SetActive(false);
        hudElement.transform.SetParent(transform);
        hudElement.GetComponent<TextMesh>().anchor = TextAnchor.MiddleCenter;
    }

    public bool IsWithinInteractionRange(Vector3 playerPosition)
    {
        return Vector3.Distance(playerPosition, transform.position) <= interactDistance;
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
        if (hudElement.activeSelf && playerCamera != null) RotateBillboardTowardsCamera();
    }

    private void RotateBillboardTowardsCamera()
    {
        Vector3 directionToCamera = playerCamera.transform.position - hudElement.transform.position;
        hudElement.transform.rotation = Quaternion.LookRotation(-directionToCamera);
    }

    public virtual void OnInteract() { }

    public void EnableInteraction()
    {
        isDisabled = false;
    }

    public void DisableInteraction()
    {
        isDisabled = true;
    }

    private void SetBillboardText()
    {
        hudElement.GetComponent<TextMesh>().text = isDisabled ? disabledPrompt : interactionPrompt;

        if (string.IsNullOrEmpty(hudElement.GetComponent<TextMesh>().text))
        {
            hudElement.SetActive(false);
        }
    }
}
