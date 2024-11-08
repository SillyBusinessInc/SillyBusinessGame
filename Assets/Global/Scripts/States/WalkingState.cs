using UnityEngine;

public class WalkingState : BaseState
{
    public WalkingState(Player player) : base(player)
    {

    }

    public override void Update()
    {
        // add force to the player object for movement
        player.playerRb.AddForce(player.GetDirection(), ForceMode.Force);

        if (!(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            player.SetState(new IdleState(player));
        }

        if(Input.GetKey(KeyCode.Space))
        {
            player.SetState(new JumpingState(player));
        }

        if (!player.isGrounded)
        {
            player.SetState(new FallingState(player));
        }
    }
}
