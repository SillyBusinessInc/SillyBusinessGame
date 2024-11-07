using JetBrains.Annotations;

public class AttackingState : BaseState
{
    public AttackingState(Player player) : base(player)
    {
    }

    public override void OnAttack()
    {
        player.SetState(new IdleState(player));
    }
}
