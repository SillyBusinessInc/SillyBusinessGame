using UnityEngine;

public static class GlobalReference
{
    private static PlayerReference player;
    public static PlayerReference Player { 
        get { return player ? player : GameObject.FindGameObjectWithTag("player").GetComponent<PlayerReference>(); }
        set { player = value; }
    }
}
