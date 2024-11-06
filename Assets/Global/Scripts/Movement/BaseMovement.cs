using UnityEngine;

public abstract class BaseMovement : MonoBehaviour
{
    public Player player;
    public BaseMovement(Player player)
    {
        this.player = player;
    }

    public void Walk()
    {
        
    }    

    public void Fall()
    {
        Debug.Log("Falling");
    }

    public void Jump()
    {
        Debug.Log("Jumping");
    }

}
