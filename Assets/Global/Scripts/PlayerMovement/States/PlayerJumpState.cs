using UnityEngine;
    
public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerMovement player, PlayerStateMachine stateMachine) 
        : base(player, stateMachine) { }
    
    public override void Enter()
    {
        // Apply jump force
        var jumpDirection = -this.Player.GravityDirection.normalized;
        jumpDirection = Vector3.Lerp(jumpDirection, Vector3.up, this.Player.jumpWorldUpPercentage);
        this.Player.Rb.AddForce(jumpDirection * this.Player.jumpForce, ForceMode.Impulse);
        
        // Immediately transition to airborne state
        this.Player.ResetJumpCooldown();
        this.StateMachine.ChangeState(PlayerStateType.Falling);
    }
}