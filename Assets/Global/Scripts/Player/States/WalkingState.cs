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
            Player.SetState(new IdleState(Player));
        }

        if(Input.GetKey(KeyCode.Space))
        {
            Player.SetState(new JumpingState(Player));
        }

        if (!Player.isGrounded)
        {
            Player.SetState(new FallingState(Player));
        }
    }
}
