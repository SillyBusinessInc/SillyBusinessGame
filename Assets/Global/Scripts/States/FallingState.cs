using UnityEngine;

public class FallingState : BaseState
{
    public FallingState(Player player) : base(player)
    {
    }

    public override void OnGround()
    {
        player.SetState(new WalkingState(player));
    }

    public override void OnAttack()
    {
        player.SetState(new AttackingState(player));
    }


}
