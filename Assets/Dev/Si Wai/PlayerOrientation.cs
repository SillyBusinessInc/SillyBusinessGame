using UnityEngine;

public class PlayerOrientation : MonoBehaviour
{
    public float sensitivityX = 200f;

    public Rigidbody playerObject;

    private float _yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        var trans = this.transform;
        
        this._yRotation += Input.GetAxis("Mouse X") * Time.deltaTime * this.sensitivityX;
        trans.localRotation = Quaternion.Euler(0f, this._yRotation, 0f);
        
        trans.position = this.playerObject.transform.position;
        trans.Translate(Vector3.forward);
    }
}
