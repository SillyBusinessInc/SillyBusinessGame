using UnityEngine;
using UnityEngine.InputSystem;

public class WalkingState : StateBase
{
    public WalkingState(Player player) : base(player)
    {

    }

    public override void Update()
    {
        // add force to the player object for movement
        Debug.Log(Player.speed);
        Debug.Log(Player.GetDirection());
        Player.rb.AddForce(Player.GetDirection() * Player.speed, ForceMode.Force);
        if (Player.inputActions.actions["Move"].ReadValue<Vector2>() == Vector2.zero)
        {
            Player.SetState(Player.states.Idle);
        }
        if(Player.inputActions.actions["Jump"].triggered)
        {
            Player.SetState(Player.states.Jumping);
        }

        if (!Player.isGrounded)
        {
            Player.SetState(Player.states.Falling);
        }
        if(Player.inputActions.actions["Dodge"].triggered && Player.canDodgeRoll)
        {
            Player.SetState(Player.states.DodgeRoll);
        }
        
    }
}
