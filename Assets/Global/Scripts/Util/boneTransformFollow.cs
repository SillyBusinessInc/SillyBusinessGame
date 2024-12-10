using UnityEngine;

public class BoneTransformFollow : MonoBehaviour
{
    [SerializeField] Transform bone;
    [SerializeField] Vector3 boneOffsetPosition = new Vector3(0f, 0f, 0f);
    [SerializeField] Vector3 boneOffsetRotation = new Vector3(0f, 0f, 0f);

    void Update()
    {
        transform.SetPositionAndRotation(bone.position + bone.rotation * boneOffsetPosition, bone.rotation * Quaternion.Euler(boneOffsetRotation));
    }
}