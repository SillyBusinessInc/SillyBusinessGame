using UnityEngine;

public class PlayerGroundState : PlayerBaseState
{
    public PlayerGroundState(PlayerMovement player, PlayerStateMachine stateMachine) 
        : base(player, stateMachine) { }
    
    public override void FixedUpdate()
    {
        // Apply full movement speed
        var moveDirection = this.Player.GetMoveDirection();
        moveDirection = moveDirection.normalized * (this.Player.RequestingSprint ? this.Player.sprintSpeed : this.Player.walkingSpeed);
        this.Player.Rb.AddForce(moveDirection, ForceMode.Force);
        
        // Check for state transitions
        if (!this.Player.IsGrounded)
        {
            this.StateMachine.ChangeState(PlayerStateType.Falling);
            return;
        }
        
        if (this.Player.RequestingJump)
            this.StateMachine.ChangeState(PlayerStateType.Jump);
    }
}