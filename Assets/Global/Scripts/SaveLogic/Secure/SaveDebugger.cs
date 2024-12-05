using UnityEngine;

public class SaveDebugger : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Testing save:...");
        GlobalReference.PlayerSave.SaveAll();
        Debug.Log("Testing load:...");
        GlobalReference.PlayerSave.LoadAll();

        Debug.Log("Displaying all ints:.......................................");
        GlobalReference.PlayerSave.ListAll<int>();
        Debug.Log("Displaying all floats:.....................................");
        GlobalReference.PlayerSave.ListAll<float>();
        Debug.Log("Displaying all strings:....................................");
        GlobalReference.PlayerSave.ListAll<string>();
        Debug.Log("Displaying all bools:......................................");
        GlobalReference.PlayerSave.ListAll<bool>();
        Debug.Log("Displaying all vector2s:...................................");
        GlobalReference.PlayerSave.ListAll<Vector2>();
        Debug.Log("Displaying all vector3s:...................................");
        GlobalReference.PlayerSave.ListAll<Vector3>();
        Debug.Log("Displaying all vector4s:...................................");
        GlobalReference.PlayerSave.ListAll<Vector4>();
    }
}
