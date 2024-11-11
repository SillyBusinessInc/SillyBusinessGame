using UnityEngine;

public class PlayerReference : Reference
{
    private Player player;
    public Player Player {
        get => player ? player : player = GetComponent<Player>();
    }

    private PlayerObjScript playerObj;
    public PlayerObjScript PlayerObj {
        get => playerObj ? playerObj : playerObj = GetComponentInChildren<PlayerObjScript>();
    }
}
