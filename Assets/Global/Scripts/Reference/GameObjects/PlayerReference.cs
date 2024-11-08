using Unity.VisualScripting;
using UnityEngine;

public class PlayerReference : Reference
{
    public Player player;
    void Start() {
        player = GetComponent<Player>();
    }
}
