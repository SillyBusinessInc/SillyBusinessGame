
using UnityEngine;

public abstract class PlayerBaseState
{
    protected readonly PlayerMovement Player;
    protected readonly PlayerStateMachine StateMachine;

    protected PlayerBaseState(PlayerMovement player, PlayerStateMachine stateMachine)
    {
        this.Player = player;
        this.StateMachine = stateMachine;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void Exit() { }

    // THIS METHOD SHOULD NOT BE CALLED, ONLY OVERWRITTEN, IT IS ALREADY BEING CALLED BY THE PLAYER-MOVEMENT CLASS
    // JUST USE E.G. `this.Player.IsGrounded` INSTEAD
    public virtual GroundCheckData DoGroundCheck()
    { 
        // This method does the ground check and returns data about this check
        // This data includes e.g. the new gravity direction and whether the player is grounded
        var grounded = Physics.Raycast(this.Player.transform.position,
                                       this.Player.GravityDirection, out var hit,
                                       this.Player.gravityFloorCheckDistance);
        var gravityDirection = grounded ? -hit.normal : Vector3.down;
        var slope = Vector3.Angle(hit.normal, Vector3.up);
        return new()
        {
            SlopeAngle = slope,
            IsGrounded = grounded,
            GravityDirection = gravityDirection,
            ModelDownDirection = gravityDirection
        };
    }

    public virtual void ApplyGravity()
    {
        var gravity = this.Player.GravityDirection.normalized * this.Player.gravityForce;
        this.Player.Rb.AddForce(gravity, ForceMode.Force);
    }
}
