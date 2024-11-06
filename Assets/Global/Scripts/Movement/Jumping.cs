using JetBrains.Annotations;
using UnityEngine;

public class Jumping : BaseMovement
{
    public Jumping(Player player) : base(player)
    {
        OnJump();
    }

    public override void OnJump()
    {
        if (player.isOnGround)
        {
            player.playerRb.AddForce(Vector3.up * player.jumpforce, ForceMode.Impulse);
        }
        player.isOnGround = false;
        player.SetState(new Falling(player));
    }
}
