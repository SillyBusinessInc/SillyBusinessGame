using UnityEngine;

public class PlayerReference : Reference
{
    private Player player;
    public Player Player {
        get => player ? player : player = GetComponent<Player>();
    }

    private PlayerObject playerObj;
    public PlayerObject PlayerObj {
        get => playerObj ? playerObj : playerObj = GetComponentInChildren<PlayerObject>();
    }
}
