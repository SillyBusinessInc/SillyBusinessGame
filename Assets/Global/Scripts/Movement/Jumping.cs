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
        player.playerRb.AddForce(Vector3.up * player.jumpforce, ForceMode.Impulse);
        player.SetState(new Falling(player));
    }
}
