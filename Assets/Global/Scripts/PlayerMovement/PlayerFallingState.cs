using UnityEngine;
    
public class PlayerFallingState : PlayerBaseState
{
    public PlayerFallingState(PlayerMovement player, PlayerStateMachine stateMachine) 
        : base(player, stateMachine) { }
    
    public override void FixedUpdate()
    {
        // Apply reduced movement speed
        var moveDirection = this.Player.GetMoveDirection();
        moveDirection = moveDirection.normalized * 
                        (this.Player.movementSpeed * this.Player.airControl);
        this.Player.Rb.AddForce(moveDirection, ForceMode.Force);
        
        // Check for ground contact
        if (this.Player.IsGrounded)
            this.StateMachine.ChangeState(PlayerStateType.Ground);
    }

    public override GroundCheckData DoGroundCheck()
    {
        // Difference from normal calculation:
        // - we are falling, so we dont have to check what the new gravity direction is, we know it is down
        // - to make it look a bit nicer, we are not directly setting the model rotation down, but we are smoothing it to the down direction
        var grounded = Physics.Raycast(this.Player.transform.position, Vector3.down, out _, 
                                       this.Player.gravityFloorCheckDistance);
        
        return new()
        {
            IsGrounded = grounded,
            GravityDirection = Vector3.down,
            ModelDownDirection = Vector3.Lerp(this.Player.ModelDownDirection, Vector3.down, this.Player.fallingRotateSpeed)
        };
    }
}