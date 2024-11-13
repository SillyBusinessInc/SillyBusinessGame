using UnityEngine;

public class IdleState : StateBase
{
    public IdleState(Player player) : base(player)
    {

    }

    public override void Update()
    {
        
        if (Player.inputActions.actions["Move"].ReadValue<Vector2>() != Vector2.zero)
        {
            Player.SetState(Player.states.Walking);
        }

        if (Player.inputActions.actions["Jump"].triggered)
        {
            Player.SetState(Player.states.Jumping);
        }
        if (!Player.isGrounded)
        {
            Player.SetState(Player.states.Falling);
        }

        if (Input.GetMouseButtonDown(0)) // TODO: replace this with the new event system thing
        {
            Player.SetState(Player.states.Attacking);
        }
        if (Player.inputActions.actions["Dodge"].triggered && Player.canDodgeRoll)
        {
            Player.SetState(Player.states.DodgeRoll);
        }
    }
}
