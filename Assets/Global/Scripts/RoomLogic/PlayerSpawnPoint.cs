using Unity.Cinemachine;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    private CinemachineCamera cinemachineCamera;
    private SmoothTarget smoothCamaraTarget;
    private Vector3 previousPosition;
    private Vector3 presentPosition;
    private Vector3 delta;

    public void Start() {
        cinemachineCamera = GlobalReference.GetReference<PlayerReference>().CinemachineCamera;
        smoothCamaraTarget = GlobalReference.GetReference<PlayerReference>().SmoothCamaraTarget;
        previousPosition = smoothCamaraTarget.transform.position;

        SpawnPoint();

        presentPosition = smoothCamaraTarget.transform.position;
        delta = presentPosition - previousPosition;

        // move the camera immidiately to the smoothCamaraTarget's transform
        cinemachineCamera.OnTargetObjectWarped(smoothCamaraTarget.transform, delta);
    }

    public void SpawnPoint() {
        Vector3 offset = new Vector3(0, 0, 3);

        var playerObj = GlobalReference.GetReference<PlayerReference>().PlayerObj;
        var rb = playerObj.GetComponent<Rigidbody>();
        rb.MovePosition(this.transform.position + offset);
        Quaternion targetRotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y + 180, this.transform.rotation.eulerAngles.z);
        rb.MoveRotation(targetRotation);

        var SmoothCamaraTarget = GlobalReference.GetReference<PlayerReference>().SmoothCamaraTarget;
        SmoothCamaraTarget.transform.position = this.transform.position +offset;
        SmoothCamaraTarget.transform.rotation = targetRotation;
    }
}
