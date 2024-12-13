using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.XR;

public class PauseLogic : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject[] UpgradeOptions;
    // public Image fadeImage;
    private bool isPaused = false;


    void Start() {
        PauseMenu.SetActive(false);
        foreach (var option in UpgradeOptions)  {
            option.SetActive(false);
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && IsLoadingSceneLoaded()) { 
            isPaused = !isPaused;
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked; 
            Cursor.visible = !Cursor.visible;

            PauseMenu.SetActive(!PauseMenu.activeSelf); 
            foreach (var option in UpgradeOptions)  {
                option.SetActive(!option.activeSelf);
            }
            Time.timeScale = isPaused ? 0f : 1f;
        }
    }

    bool IsLoadingSceneLoaded() { 
        for (int i = 0; i < SceneManager.sceneCount; i++) { 
            Scene scene = SceneManager.GetSceneAt(i); 
            if (scene.name == "Loading") { 
                return false; 
            }
        }
        return true; 
    }

    public void ContinueGame() {
        Debug.Log(" !!! ContinueGame !!! ");
        isPaused = false;
        PauseMenu.SetActive(!PauseMenu.activeSelf); 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    public void Settings() {
        PauseMenu.SetActive(!PauseMenu.activeSelf); 
        SceneManager.LoadScene("Settings");
        // UILogic.FadeToScene("Settings", fadeImage, this);
    }

    public void QuitGame() {
        PauseMenu.SetActive(!PauseMenu.activeSelf); 
        SceneManager.LoadScene("Menu");
        // UILogic.FadeToScene("Menu", fadeImage, this);
    }

}
