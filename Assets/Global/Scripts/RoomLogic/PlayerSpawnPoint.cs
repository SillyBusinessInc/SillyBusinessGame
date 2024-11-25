using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawnPoint : MonoBehaviour
{
    public void Awake() {
        SpawnPoint();
    }

    public void SpawnPoint() {
        Scene BaseScene = SceneManager.GetSceneByName("BaseScene");
        GameObject[] rootObjects = BaseScene.GetRootGameObjects();
        foreach (GameObject obj in rootObjects)
        {
            if (obj.CompareTag("PlayerPrefeb")) {
                foreach (Transform child in obj.transform)
                {
                    if (child.CompareTag("Player"))
                    {
                        child.transform.position = this.transform.position;
                        // Debug.Log("child.name : " + child.name);
                        Debug.Log("spawnPoint position = " + this.transform.position);
                        Debug.Log("child position = " + child.transform.position);
                        break;
                    }
                }
            }
        }
    }
}
