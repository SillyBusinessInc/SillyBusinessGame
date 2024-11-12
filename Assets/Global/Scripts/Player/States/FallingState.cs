using UnityEngine;
using UnityEngine.InputSystem;

public class FallingState : StateBase
{
    public FallingState(Player player) : base(player)
    {
    }
    
    public override void Update()
    {
        Player.rb.AddForce(Player.GetDirection() * (Player.speed * Player.airBornMovementFactor), ForceMode.Force);

        if(Player.inputActions.actions["Jump"].triggered && Player.doubleJumps > Player.currentJumps)
        {
            Player.SetState(Player.states.Jumping);
            Player.currentJumps += 1; 
        }
        if(Player.inputActions.actions["Glide"].ReadValue<float>() != 0 && Player.rb.linearVelocity.y < 0 && Player.canDodgeRoll)
        {
            Player.SetState(Player.states.Gliding);
        }
        if(Player.inputActions.actions["Dodge"].triggered && Player.canDodgeRoll)
        {
            Player.SetState(Player.states.DodgeRoll);
        }
    }

    public override void OnCollision(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Player.SetState(Player.states.Idle);
            Player.currentJumps = 0;
        }
    }
}
