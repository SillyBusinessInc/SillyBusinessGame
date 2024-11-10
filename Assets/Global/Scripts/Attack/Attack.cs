using UnityEngine;
public class Attack : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 10f;
    public bool rotateLeft;

    public float rotation;

    void FixedUpdate()
    {
        transform.RotateAround(target.position, Vector3.up, rotation);
        rotation += 1;
    }
}