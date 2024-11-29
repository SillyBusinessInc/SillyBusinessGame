using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class FallingState : StateBase
{
    public FallingState(Player player) : base(player) {}

    public override void Update()
    {
        // preserve y velocity
        float linearY = Player.rb.linearVelocity.y;

        // add extra gravity to prevent floatiness
        if (linearY < Player.jumpVelocityFalloff || linearY > 0 && !Player.isHoldingJump && !Player.isHoldingDodge) {
            linearY += Player.fallMultiplier * Physics.gravity.y * Time.deltaTime;
            Player.debug_lineColor = Color.red;
        }
        else {
            Player.debug_lineColor = Color.yellow;
        }

        // apply horizontal momentum based on input
        Vector3 newTargetVelocity = Player.GetDirection() * (Player.playerStatistic.Speed.GetValue() * Player.airBorneMovementFactor);
        Player.targetVelocity = new(newTargetVelocity.x, linearY, newTargetVelocity.z);

        // change state on ground
        if (Player.isGrounded) Player.SetState(Player.movementInput.magnitude > 0 ? Player.states.Walking : Player.states.Idle);
    }

    public override void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.started && Player.playerStatistic.DoubleJumpsCount.GetValueInt() > Player.currentJumps)
        {
            Player.currentJumps += 1;
            Player.isHoldingJump = true;
            Player.SetState(Player.states.Jumping);
        }
        if (ctx.canceled) 
        {
            Player.isHoldingJump = false;
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
