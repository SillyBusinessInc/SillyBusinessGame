using UnityEngine;

public abstract class BaseMovement : MonoBehaviour
{
    public Player player;
    public BaseMovement(Player player)
    {
        this.player = player;
    }

    public virtual void OnAttack()
    {
        
    }    

    public virtual void OnJump()
    {
        
    }

    public virtual void OnWalk()
    {
        
    }

    public virtual void OnGround()
    {

    }

}
