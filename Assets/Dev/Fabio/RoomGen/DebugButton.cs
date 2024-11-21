using TMPro;
using UnityEngine;

public class DebugButton : MonoBehaviour
{
    public string id;
    void Start()
    {
        GetComponent<TMP_InputField>().text = GlobalReference.DevSettings.Get<int>(id).ToString();
    }
}
