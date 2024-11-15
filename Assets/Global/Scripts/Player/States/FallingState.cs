using UnityEngine;
using UnityEngine.InputSystem;

public class FallingState : StateBase
{
    public FallingState(Player player) : base(player) { }

    public override void Enter()
    {
        // Subscribe to input action events for Jump, Glide, Dodge, and Attack
        Player.inputActions.actions["Glide"].performed += OnGlide;
        Player.inputActions.actions["Jump"].performed += OnJump;
        Player.inputActions.actions["Dodge"].performed += OnDodge;
        Player.inputActions.actions["Attack"].performed += OnAttack;
    }

    public override void Exit()
    {
        // Unsubscribe from input action events to avoid multiple subscriptions
        Player.inputActions.actions["Glide"].performed -= OnGlide;
        Player.inputActions.actions["Jump"].performed -= OnJump;
        Player.inputActions.actions["Dodge"].performed -= OnDodge;
        Player.inputActions.actions["Attack"].performed -= OnAttack;
    }

    public override void Update()
    {
        // Apply horizontal movement in the air
        Player.rb.AddForce(Player.GetDirection() * (Player.playerStatistic.speed * Player.airBornMovementFactor), ForceMode.Force);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (Player.doubleJumps > Player.currentJumps)
        {
            Player.SetState(Player.states.Jumping);
            Player.currentJumps += 1;
        }
    }

    private void OnGlide(InputAction.CallbackContext context)
    {
        // if (Player.rb.linearVelocity.y < 0 && Player.canDodgeRoll)
        // {
        Player.SetState(Player.states.Gliding);
        // }
    }

    private void OnDodge(InputAction.CallbackContext context)
    {
        if (Player.canDodgeRoll)
        {
            Player.SetState(Player.states.DodgeRoll);
        }
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        Player.attackCounter = 2;
        Player.isSlamming = true;
        Player.SetState(Player.states.Attacking);
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
