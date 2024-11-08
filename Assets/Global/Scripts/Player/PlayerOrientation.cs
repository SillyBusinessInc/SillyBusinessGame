using System;
using UnityEngine;

public class PlayerOrientation : MonoBehaviour
{
    [SerializeField] private float sensitivityX = 200f;
    [SerializeField] private Rigidbody playerObject;

    private float _yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
 
        _yRotation += Input.GetAxis("Mouse X") * Time.deltaTime * sensitivityX;
        transform.localRotation = Quaternion.Euler(0f, _yRotation, 0f);
        
        transform.position = playerObject.transform.position;
        transform.Translate(Vector3.forward);
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.forward,
            Color.red, 0f, false);
    }
}
