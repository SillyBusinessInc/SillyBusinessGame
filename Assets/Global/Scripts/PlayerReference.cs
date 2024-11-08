using UnityEngine;

public class PlayerReference : MonoBehaviour
{
    public int hp = 10;
    void Awake()
    {
        GlobalReference.Player = this;
    }

    void OnDestroy() {
        GlobalReference.Player = null;
    }

    public void Heal(int n) {
        hp += n;
    }
}
