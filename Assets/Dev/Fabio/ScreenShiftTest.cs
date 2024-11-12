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

    public void Warn() {
        Debug.LogWarning("something");
    }

    void Start() {
        GlobalReference.SubscribeTo(Events.PICKUP_COLLECTED, Scream);
        GlobalReference.SubscribeTo(Events.PICKUP_COLLECTED, Warn);
    }
}
