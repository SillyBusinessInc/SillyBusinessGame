using UnityEngine;

public class Falling : BaseMovement
{
    public Falling(Player player) : base(player)
    {
    }

    public override void OnGround()
    {
        player.SetState(new Walking(player));
    }

    public override void OnAttack()
    {
        player.SetState(new Attacking(player));
    }

}
