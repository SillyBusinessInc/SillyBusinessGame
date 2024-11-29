using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawnPoint : MonoBehaviour
{
    public void Awake() {
        SpawnPoint();
    }

    public void SpawnPoint() {
        var playerObj = GlobalReference.GetReference<PlayerReference>().PlayerObj;
        playerObj.transform.position = this.transform.position;

        var SmoothCamaraTarget = GlobalReference.GetReference<PlayerReference>().SmoothCamaraTarget;
        SmoothCamaraTarget.transform.position = this.transform.position;
    }
}
