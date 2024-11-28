using UnityEngine;
using UnityEngine.InputSystem;

public class FallingState : StateBase
{
    public FallingState(Player player) : base(player) { }

    public override void Update()
    {
        // Apply horizontal movement in the air
        Player.rb.AddForce(Player.GetDirection() * (Player.playerStatistic.Speed.GetValue() * Player.airBornMovementFactor), ForceMode.Acceleration);
        if(Player.isGrounded)
        {
            Player.SetState(Player.movementInput.magnitude > 0 ? Player.states.Walking : Player.states.Idle);
        }
    }

    public override void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.started && Player.playerStatistic.DoubleJumpsCount.GetValueInt() > Player.currentJumps)
        {
            Player.currentJumps += 1;
            Player.SetState(Player.states.Jumping);
        }
    }

    public override void Glide(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && Player.rb.linearVelocity.y < 0)
        {
            Player.SetState(Player.states.Gliding);
        }
    }
}
