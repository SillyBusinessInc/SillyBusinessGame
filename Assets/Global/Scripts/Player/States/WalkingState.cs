using UnityEngine;
using UnityEngine.InputSystem;

public class WalkingState : StateBase
{
    public WalkingState(Player player) : base(player)
    {
    }

    public override void Update()
    {
        Player.playerAnimationsHandler.resetStates();
        Player.playerAnimationsHandler.SetBool("IsRunning", true);
        // add force to the player object for movement
        Player.rb.AddForce(Player.GetDirection() * Player.playerStatistic.Speed.GetValue(), ForceMode.Acceleration);
        if (!Player.isGrounded)
        {
            Player.SetState(Player.states.Falling);
        }
    }
    public override void Jump(InputAction.CallbackContext ctx)
    {
        Player.playerAnimationsHandler.SetBool("IsFallingDown", false);
        Player.playerAnimationsHandler.SetBool("IsJumpingBool",true);

        base.Jump(ctx);
    }

}
