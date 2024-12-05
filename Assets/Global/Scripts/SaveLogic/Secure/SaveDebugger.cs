using UnityEngine;

public class SaveDebugger : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Testing save:...");
        GlobalReference.PlayerSave.SaveAll();
        Debug.Log("Testing load:...");
        GlobalReference.PlayerSave.LoadAll();
    }
}
