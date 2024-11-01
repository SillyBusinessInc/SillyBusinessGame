using UnityEngine;
    
public class PlayerFallingState : PlayerBaseState
{
    public PlayerFallingState(PlayerMovement player, PlayerStateMachine stateMachine) 
        : base(player, stateMachine) { }
    
    public override void FixedUpdate()
    {
        // Apply reduced movement speed
        var moveDirection = this.Player.GetMoveDirection();
        moveDirection = moveDirection.normalized * (this.Player.airControl *
                                                    (this.Player.RequestingSprint ? this.Player.sprintSpeed : this.Player.walkingSpeed));
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
        var grounded = Physics.Raycast(this.Player.Rb.transform.position, Vector3.down, out var hit, 
                                       this.Player.gravityFloorCheckDistance);
        var slope = Vector3.Angle(hit.normal, Vector3.up);
        return new()
        {
            SlopeAngle = slope,
            IsGrounded = grounded,
            GravityDirection = Vector3.down,
            ModelDownDirection = Vector3.Lerp(this.Player.ModelDownDirection, Vector3.down, this.Player.fallingRotateSpeed)
        };
    }
}