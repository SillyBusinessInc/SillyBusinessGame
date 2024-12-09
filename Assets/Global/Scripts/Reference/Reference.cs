using UnityEngine;

public abstract class Reference : MonoBehaviour
{
    void Awake() => GlobalReference.RegisterReference(this);
    void OnDestroy() => GlobalReference.UnregisterReference(this);
}