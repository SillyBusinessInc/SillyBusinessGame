using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public CinemachineBrain cinemachineBrain;
    public CinemachineCamera cinemachineCamera;
    public Transform target;

    // void Start()
    // {
    //     SetHardLookAt();
    // }

    // public void SetHardLookAt()
    // {
    //     cinemachineCamera.Follow = target;
    //     cinemachineCamera.LookAt = target;
    // }

    // public void SetManualPositionAndRotation(Vector3 customPosition, Quaternion customRotation)
    // {
    //     Debug.Log("SetManualPositionAndRotation works");
    //     cinemachineCamera.Follow = null;
    //     cinemachineCamera.LookAt = null;

    //     cinemachineCamera.transform.position = customPosition;
    //     cinemachineCamera.transform.rotation = customRotation;

    //     SetHardLookAt();
    // }
}
