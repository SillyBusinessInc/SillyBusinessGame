using System.Data;
using UnityEngine;
using UnityEngine.UIElements;
public class Attack : MonoBehaviour
{
    public Transform target;
    public bool rotateLeft;

    public float rotation;

    public float speed;

    void FixedUpdate()
    {
        for (int i = 0; i < speed; i++) //Forloop to make the rotation speed more smooth
        {
            if(rotation >= 180.0f)
            {
                rotateLeft = true;
            }
            if(rotation <= 0.0f)
            {
                rotateLeft = false;
            }
            if (rotateLeft)
            {
                transform.RotateAround(target.position, Vector3.up, 1);
                rotation -= 1;
            }
            else
            {
                transform.RotateAround(target.position, Vector3.up, -1);
                rotation += 1;
            }
            Debug.Log(rotation);
        }
    }
}