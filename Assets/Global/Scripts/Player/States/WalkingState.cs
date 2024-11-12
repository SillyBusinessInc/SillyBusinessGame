using UnityEngine;

public class WalkingState : StateBase
{
    public WalkingState(Player player) : base(player)
    {

    }

    public override void Update()
    {
        // add force to the player object for movement
        Player.rb.AddForce(Player.GetDirection() * Player.speed, ForceMode.Force);

        if (!(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            Player.SetState(Player.states.Idle);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Player.SetState(Player.states.Jumping);
        }

        if (!Player.isGrounded)
        {
            Player.SetState(Player.states.Falling);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Player.SetState(Player.states.Attacking);
        }
        if(Input.GetKeyDown(KeyCode.E) && Player.canDodgeRoll)
        {
            Player.SetState(Player.states.DodgeRoll);
        }
    }
}
