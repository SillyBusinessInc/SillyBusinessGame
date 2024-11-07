using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    public Player player;
    public BaseState(Player player)
    {
        this.player = player;
    }

    public virtual void Enter()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void FixedUpdate()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void OnCollision(Collision collision)
    {

    }
}
