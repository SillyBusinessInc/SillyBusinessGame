using UnityEngine;
using UnityEngine.InputSystem;

public class FallingState : StateBase
{
    public FallingState(Player player) : base(player) { 
        
    }

    public override void Enter()
    {
        // Player.playerAnimationsHandler.resetStates();
        Debug.Log("FallingState");
        Player.playerAnimationsHandler.SetBool("IsFallingDown", true);

    }

    public override void Update()
    {
        // Player.playerAnimationsHandler.resetStates();
        // Apply horizontal movement in the air
        AnimatorStateInfo stateInfo = Player.playerAnimationsHandler.animator.GetCurrentAnimatorStateInfo(0); // Layer 0 is usually the default layer
        if (stateInfo.IsName("Breadaplus|Bradley_jump_from_ground 0")) // Replace "AnimationName" with your animation's name
        {
            if (stateInfo.normalizedTime >= 1.0f && !Player.playerAnimationsHandler.animator.IsInTransition(0))
            {
                Debug.Log("Animation finished!");
                Player.playerAnimationsHandler.SetBool("IsJumpingBool",false);
            }
        }
        Player.rb.AddForce(Player.GetDirection() * (Player.playerStatistic.Speed.GetValue() * Player.airBornMovementFactor), ForceMode.Acceleration);
        if(Player.isGrounded)
        {
            Player.SetState(Player.movementInput.magnitude > 0 ? Player.states.Walking : Player.states.Idle);
        }
    }

    public override void Jump(InputAction.CallbackContext ctx)
    {
        // air jump animation 
        if (ctx.started && Player.playerStatistic.DoubleJumpsCount.GetValueInt() > Player.currentJumps)
        {
            Player.playerAnimationsHandler.animator.SetTrigger("IsDoubleJumping");
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
