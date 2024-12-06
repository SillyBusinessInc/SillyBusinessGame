using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    public void Start() {
        SpawnPoint();
    }

    public void SpawnPoint() {
        Vector3 offset = new Vector3(0, 0, 3);

        var playerObj = GlobalReference.GetReference<PlayerReference>().PlayerObj;
        var rb = playerObj.GetComponent<Rigidbody>();
        rb.MovePosition(this.transform.position + offset);
        Quaternion targetRotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y + 180, this.transform.rotation.eulerAngles.z);
        rb.MoveRotation(targetRotation);


        // var SmoothCamaraTarget = GlobalReference.GetReference<PlayerReference>().SmoothCamaraTarget;
        // var cameraRb = SmoothCamaraTarget.GetComponent<Rigidbody>();
        // cameraRb.MovePosition(this.transform.position + offset);
        // SmoothCamaraTarget.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y + 180, this.transform.rotation.eulerAngles.z);
        // SmoothCamaraTarget.transform.rotation = this.transform.rotation;

        // var playerCamera = GlobalReference.GetReference<PlayerReference>().PlayerCamera;
        // playerCamera.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y + 180, this.transform.rotation.eulerAngles.z);

    }
}
