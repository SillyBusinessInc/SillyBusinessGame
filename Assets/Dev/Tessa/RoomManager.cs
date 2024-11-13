using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public string nextRoomName; 
    private bool isTransitioning = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTransitioning) 
        {
            isTransitioning = true;
            LoadNextRoom();
        }
    }

    private void LoadNextRoom()
    {
        StartCoroutine(LoadRoomCoroutine());
    }

    private System.Collections.IEnumerator LoadRoomCoroutine()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextRoomName, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.UnloadSceneAsync(currentScene);

        Scene newScene = SceneManager.GetSceneByName(nextRoomName);
        SceneManager.SetActiveScene(newScene);

        isTransitioning = false;
    }
}
