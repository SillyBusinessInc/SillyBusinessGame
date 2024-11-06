using JetBrains.Annotations;

public class Attacking : BaseMovement
{
    public Attacking(Player player) : base(player)
    {
    }

    public override void OnAttack()
    {
        //add functionality
        player.SetState(new Idle(player));
    }
}
