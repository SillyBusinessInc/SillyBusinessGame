using UnityEngine;

public abstract class Reference : MonoBehaviour
{
    void Awake() {
        GlobalReference.Register(this);
    }

    void OnDestroy() {
        GlobalReference.Unregister(this);
    }
}
