using UnityEngine;

public abstract class StateBase
{
    protected readonly Player Player;

    protected StateBase(Player player)
    {
        this.Player = player;
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
