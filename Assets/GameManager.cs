using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Function to restart the current scene
    public void RestartGame()
    {
        // Get the current scene and reload it
        SceneManager.LoadScene("CarloLevel1");
    }

    // Function to quit the application
    public void QuitGame()
    {
        // Log quit action in editor for debugging
        Debug.Log("Quit Game");
        // Quit the application
        Application.Quit();
    }
}