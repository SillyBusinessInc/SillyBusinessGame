using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenShiftTest : MonoBehaviour
{
    public string scene;

    [ContextMenu("Shift")]
    public void Shift() {
        SceneManager.LoadScene(scene);
    }


    public void Scream() {
        Debug.Log("AAAAAAAAAAAAAAAAAH");
    }

    void Start() {
        GlobalReference.SubscribeTo("pickup", Scream);
    }
}
