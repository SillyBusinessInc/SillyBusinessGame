using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    public Player player;
    public BaseState(Player player)
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

    public virtual void Still()
    {

    }
}
