using UnityEngine;

public class IdleState : BaseState
{
    public IdleState(Player player) : base(player)
    {

    }

    public override void OnWalk()
    {
        player.SetState(new WalkingState(player));
    }

    public override void OnJump()
    {
        player.SetState(new JumpingState(player));
    }

    public override void OnAttack()
    {
        player.SetState(new AttackingState(player));
    }

}
