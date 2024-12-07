using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawnPoint : MonoBehaviour
{
    public void Start() {
        SpawnPoint();
    }

    public void SpawnPoint() {
        Vector3 offset = new Vector3(0, 0, -4);

        var playerObj = GlobalReference.GetReference<PlayerReference>().PlayerObj;
        playerObj.transform.position = this.transform.position + offset;
        playerObj.GetComponent<Rigidbody>().transform.position = this.transform.position + offset;
        // playerObj.transform.rotation = this.transform.rotation;

        var SmoothCamaraTarget = GlobalReference.GetReference<PlayerReference>().SmoothCamaraTarget;
        SmoothCamaraTarget.transform.position = this.transform.position + offset;
        // SmoothCamaraTarget.transform.rotation = this.transform.rotation;
    }
}
