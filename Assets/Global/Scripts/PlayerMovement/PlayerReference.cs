using UnityEngine;

public class PlayerReference : MonoBehaviour
{
    void Awake()
    {
        GlobalReference.Player = this;
    }

    void OnDestroy() {
        GlobalReference.Player = null;
    }
}
