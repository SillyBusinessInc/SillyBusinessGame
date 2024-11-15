using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOrientation : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private PlayerInput input;
    [SerializeField] private Transform cameraTransform;

    private Vector2 _rotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        input = GetComponentInParent<PlayerInput>();
    }

    private void Update()
    {
        // Check if the player has pressed the forward movement input
        Vector2 movementInput = input.actions["Move"].ReadValue<Vector2>();

        if (movementInput.magnitude > 0)
        {
            // Align the player with the camera's forward direction if forward movement is initiated
            AlignPlayerWithCamera();
            transform.position = playerRb.transform.position;
        }
    }

    private void AlignPlayerWithCamera()
    {
        // Get the forward direction of the camera and ignore the Y component
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        // Update the player's rotation to face the camera's horizontal direction
        if (cameraForward != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
            transform.rotation = targetRotation;
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.forward,
            Color.red, 0f, false);
    }
}
