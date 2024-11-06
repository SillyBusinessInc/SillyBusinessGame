using UnityEngine;

public class Idle : BaseMovement
{
    public Idle(Player player) : base(player)
    {

    }

    public override void OnWalk()
    {
        player.SetState(new Walking(player));
    }

    public override void OnJump()
    {
        player.SetState(new Jumping(player));
    }

    public override void OnAttack()
    {
        player.SetState(new Attacking(player));
    }
}
