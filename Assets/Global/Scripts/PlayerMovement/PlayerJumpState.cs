using UnityEngine;
    
public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerMovement player, PlayerStateMachine stateMachine) 
        : base(player, stateMachine) { }
    
    public override void Enter()
    {
        // Apply jump force
        Vector3 jumpDirection = -this.Player.GravityDirection.normalized;
        jumpDirection = Vector3.Lerp(jumpDirection, Vector3.up, this.Player.jumpWorldUpPercentage);
        this.Player.Rb.AddForce(jumpDirection * (this.Player.jumpForce * 100f), ForceMode.Force);
        
        // Immediately transition to airborne state
        this.StateMachine.ChangeState(PlayerStateType.Falling);
    }
}