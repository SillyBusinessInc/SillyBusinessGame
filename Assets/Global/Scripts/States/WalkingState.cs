using UnityEngine;

public class WalkingState : BaseState
{
    public WalkingState(Player player) : base(player)
    {

    }

    public override void Update()
    {
        player.playerRb.AddForce(player.getDirection(), ForceMode.Force);
        // if (Input.GetKey(KeyCode.W))
        //     player.playerRb.AddForce(player.getDirection() * player.speed, ForceMode.Force); // go forward
        // if (Input.GetKey(KeyCode.S))
        //     player.playerRb.AddForce(-player.getDirection() * player.speed, ForceMode.Force); // go back

        // Vector3 leftDirection = Vector3.Cross(player.getDirection(), Vector3.up);
        // if (Input.GetKey(KeyCode.A)) {
        //     player.playerRb.AddForce(leftDirection * player.speed, ForceMode.Force); // go left
        // }
        // if (Input.GetKey(KeyCode.D)) {
        //     player.playerRb.AddForce(-leftDirection * player.speed, ForceMode.Force); // go right
        // }





        
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
