using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab; 
    public string spawnPointName = "SpawnPoint"; 
    private GameObject playerInstance;

    private void Start()
    {
        playerInstance = GameObject.Find("Player");
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        GameObject spawnPoint = GameObject.Find(spawnPointName);
        
        if (spawnPoint != null && playerInstance != null)
        {
            playerInstance.transform.position = spawnPoint.transform.position;
            playerInstance.transform.rotation = spawnPoint.transform.rotation;     
        }
        else
        {
            Debug.LogWarning("No SpawnPoint");
        }
    }
}
