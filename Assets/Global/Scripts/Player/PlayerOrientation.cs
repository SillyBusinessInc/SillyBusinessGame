using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOrientation : MonoBehaviour
{
    [Range(0f, 1000f)]
    [SerializeField] private float sensitivityX = 200f;
    [Range(0f, 1000f)]
    [SerializeField] private float sensitivityY = 200f;
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] PlayerInput input;

    private Vector2 _rotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        input = GetComponentInParent<PlayerInput>();
    }

    // private void Update()
    // {
    //     Vector2 inputValue = input.actions["Look"].ReadValue<Vector2>();
    //     _rotation.x += inputValue.x * Time.deltaTime * sensitivityX;
    //     _rotation.y += inputValue.y * Time.deltaTime * sensitivityY;

    //     _rotation.y = math.clamp(_rotation.y, -90f, 90f);

    //     transform.localRotation = Quaternion.Euler(_rotation.y, _rotation.x, 0f);

    //     transform.position = playerRb.transform.position;
    //     transform.Translate(Vector3.forward);
    // }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.forward,
            Color.red, 0f, false);
    }
}
