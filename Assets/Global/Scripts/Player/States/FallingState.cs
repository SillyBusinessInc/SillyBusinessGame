using UnityEngine;

public class FallingState : StateBase
{
    public FallingState(Player player) : base(player)
    {
    }

    public override void Update()
    {
        Player.rb.AddForce(Player.GetDirection() * (Player.speed * Player.airBornMovementFactor), ForceMode.Force);

        if (Input.GetKeyDown(KeyCode.Space) && Player.doubleJumps > Player.currentJumps)
        {
            Player.SetState(Player.states.Jumping);
            Player.currentJumps += 1;
        }

        if (Input.GetKey(KeyCode.LeftShift) && Player.rb.linearVelocity.y < 0)
        {
            Player.SetState(Player.states.Gliding);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Player.attackCounter = 2;
            Player.isSlamming = true;
            Player.SetState(Player.states.Attacking);
        }
        if (Input.GetKey(KeyCode.LeftShift) && Player.rb.linearVelocity.y < 0 && Player.canDodgeRoll)
        {
            Player.SetState(Player.states.Gliding);
        }
        if (Input.GetKeyDown(KeyCode.E) && Player.canDodgeRoll)
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
