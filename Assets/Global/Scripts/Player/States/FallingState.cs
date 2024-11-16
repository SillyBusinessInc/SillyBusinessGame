using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class FallingState : StateBase
{
    public FallingState(Player player) : base(player) { }

    public override void Update()
    {
        // Apply horizontal movement in the air
        Player.rb.AddForce(Player.GetDirection() * (Player.playerStatistic.speed * Player.airBornMovementFactor), ForceMode.Force);
    }

    public override void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.started && Player.doubleJumps > Player.currentJumps)
        {
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

    public override void OnCollision(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Player.SetState(movementInput.magnitude > 0 ? Player.states.Walking : Player.states.Idle);
        }
    }
}
