using UnityEngine;

public class IdleState : StateBase
{
    public IdleState(Player player) : base(player)
    {

    }

    public override void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            Player.SetState(Player.states.Walking);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Player.SetState(Player.states.Jumping);
        }
        if (!Player.isGrounded)
        {
            Player.SetState(Player.states.Falling);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Player.SetState(Player.states.Attacking);
        }
        if(Input.GetKeyDown(KeyCode.E) && Player.canDodgeRoll)
        {
            Player.SetState(Player.states.DodgeRoll);
        }
    }

}
