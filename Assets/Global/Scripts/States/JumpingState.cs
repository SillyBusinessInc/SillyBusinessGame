using JetBrains.Annotations;
using UnityEngine;

public class JumpingState : BaseState
{
    public JumpingState(Player player) : base(player)
    {

    }

    public override void Update()
    {
        if (player.isOnGround)
        {
            player.playerRb.AddForce(Vector3.up * player.jumpforce, ForceMode.Impulse);
        }
        player.isOnGround = false;
        player.SetState(new FallingState(player));
    }
    public void OnJump()
    {
        
    }
}
