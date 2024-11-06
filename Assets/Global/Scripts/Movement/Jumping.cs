using JetBrains.Annotations;
using UnityEngine;

public class Jumping : BaseMovement
{
    public Jumping(Player player) : base(player)
    {
    }

    public override void OnJump()
    {
        //add functionality
        player.SetState(new Falling(player));
    }
}
