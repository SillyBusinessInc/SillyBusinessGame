using UnityEngine;

public class boneTransformFollow : MonoBehaviour
{
    [SerializeField]
    Transform bone;
    [SerializeField]
    Vector3 boneOffsetPosition = new Vector3(0f, 0f, 0f);
    [SerializeField]
    Vector3 boneOffsetRotation = new Vector3(0f, 0f, 0f);

    void Update()
    {
        this.transform.position = bone.position + bone.rotation * boneOffsetPosition;
        this.transform.rotation = bone.rotation * Quaternion.Euler(boneOffsetRotation);
    }
}