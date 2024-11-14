using UnityEngine;

public class ManualEnemyDebug : MonoBehaviour
{
    void Awake() {
        GlobalReference.SubscribeTo(Events.WAVE_DONE, () => Debug.Log("Wave is done!"));
    }
}
