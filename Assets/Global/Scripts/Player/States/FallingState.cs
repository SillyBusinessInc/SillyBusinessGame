using UnityEngine;
using UnityEngine.InputSystem;

public class FallingState : StateBase
{
    public FallingState(Player player)
        : base(player) { }

    public override void Update()
    {
        Player.rb.AddForce(
            Player.GetDirection() * (Player.playerStatistic.speed * Player.airBornMovementFactor),
            ForceMode.Force
        );

        if (
            Player.inputActions.actions["Glide"].ReadValue<float>() != 0
            && Player.rb.linearVelocity.y < 0
            && Player.canDodgeRoll
        )
        {
            Player.SetState(Player.states.Gliding);
        }
        else if (
            Player.inputActions.actions["Jump"].triggered
            && Player.doubleJumps > Player.currentJumps
        )
        {
            Player.SetState(Player.states.Jumping);
            Player.currentJumps += 1;
        }
        else if (Player.inputActions.actions["Dodge"].triggered && Player.canDodgeRoll)
        {
            Player.SetState(Player.states.DodgeRoll);
        }
        if (Input.GetMouseButtonDown(0)) // TODO: replace this with the new event system thing
        {
            Player.attackCounter = 2;
            Player.isSlamming = true;
            Player.SetState(Player.states.Attacking);
        }
        if (Player.isGrounded)
        {
            Player.SetState(Player.states.Idle);
            Player.currentJumps = 0;
        }
    }
}
