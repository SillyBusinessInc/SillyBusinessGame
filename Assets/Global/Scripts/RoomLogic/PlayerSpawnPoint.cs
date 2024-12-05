using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawnPoint : MonoBehaviour
{
    public void Start() {
        SpawnPoint();
    }

    public void SpawnPoint() {
        Vector3 offset = new Vector3(0, 0, 3);

        var playerObj = GlobalReference.GetReference<PlayerReference>().PlayerObj;
        playerObj.transform.position = this.transform.position + offset;
        playerObj.GetComponent<Rigidbody>().transform.position = this.transform.position + offset;
        playerObj.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y + 180, this.transform.rotation.eulerAngles.z);

        // var SmoothCamaraTarget = GlobalReference.GetReference<PlayerReference>().SmoothCamaraTarget;
        // SmoothCamaraTarget.transform.position = this.transform.position + offset;
        // SmoothCamaraTarget.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y + 180, this.transform.rotation.eulerAngles.z);
        // // SmoothCamaraTarget.transform.rotation = this.transform.rotation;

        var playerCamera = GlobalReference.GetReference<PlayerReference>().PlayerCamera;
        playerCamera.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y + 180, this.transform.rotation.eulerAngles.z);

    }
}
